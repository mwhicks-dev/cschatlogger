using Microsoft.AspNetCore.Mvc;
using CSChatLogger.Api;
using CSChatLogger.Schema;
using CSChatLogger.Entity;

namespace Api.Controllers
{
    [Route("/cschat/1/chat")]
    [ApiController]
    public class ChatMessageController(Context context) : ControllerBase
    {
        private readonly Context _context = context;

        [HttpPost("{chat_id}/message")]
        public async Task<IActionResult> SendChatMessage(Guid? token, long chat_id, SendChatMessageInput dto)
        {
            if (token == null)
                return Unauthorized();

            long userId = GetUserId((Guid)token);

            if (!GetUserIsInChat(userId, chat_id).Result)
                return Unauthorized();

            ChatMessage chatMessage = new();
            chatMessage.UserId = userId;
            chatMessage.ChatId = chat_id;
            chatMessage.Message = dto.message;
            chatMessage.DateTime = DateTime.UtcNow;
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{chat_id}/message")]
        public async Task<ActionResult<ReadChatMessagesOutput>> ReadChatMessages(Guid? token, long chat_id)
        {
            if (token == null)
                return Unauthorized();

            long userId = GetUserId((Guid)token);

            if (!GetUserIsInChat(userId, chat_id).Result)
                return Unauthorized();

            var dbChatMessages = await _context.FindAsync<IEnumerable<ChatMessage>>();

            ReadChatMessagesOutput output = new()
            {
                messages = []
            };

            if (dbChatMessages != null)
            {
                foreach (ChatMessage message in dbChatMessages)
                {
                    if (message.ChatId == chat_id)
                    {
                        MessageDto messageDto = new()
                        {
                            sender = message.UserId,
                            datetime = message.DateTime,
                            id = message.MessageId,
                            message = message.Message
                        };
                    }
                }
            }

            return output;
        }

        [HttpPut("{chat_id}/message/{message_id}")]
        public async Task<IActionResult> UpdateChatMessage(Guid? token, long chat_id, long message_id, UpdateChatMessageInput dto)
        {
            if (token == null)
                return Unauthorized();

            long userId = GetUserId((Guid)token);

            if (!GetUserIsInChat(userId, chat_id).Result)
                return Unauthorized();

            var chatMessages = await _context.FindAsync<IEnumerable<ChatMessage>>();
            if (chatMessages == null)
                return NotFound();

            ChatMessage? chatMessage = null;
            foreach (ChatMessage tmp in chatMessages)
            {
                if (tmp.MessageId == message_id)
                {
                    if (tmp.UserId != userId)
                        return Unauthorized();

                    chatMessage = tmp;
                    break;
                }
            }

            if (chatMessage == null)
                return NotFound();

            chatMessage.Message = dto.message;
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{chat_id}/message/{message_id}")]
        public async Task<IActionResult> DeleteChatMessage(Guid? token, long chat_id, long message_id)
        {
            if (token == null)
                return Unauthorized();

            long userId = GetUserId((Guid)token);

            if (!GetUserIsInChat(userId, chat_id).Result)
                return Unauthorized();

            var chatMessages = await _context.FindAsync<IEnumerable<ChatMessage>>();
            if (chatMessages == null)
                return NotFound();

            ChatMessage? chatMessage = null;
            foreach (ChatMessage tmp in chatMessages)
            {
                if (tmp.MessageId == message_id)
                {
                    if (tmp.UserId != userId)
                        return Unauthorized();

                    chatMessage= tmp;
                    break;
                }
            }

            if (chatMessage == null)
                return NotFound();

            _context.ChatMessages.Remove(chatMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static long GetUserId(Guid token)
        {
            // TODO: Implement
            return -1;
        }

        private async Task<bool> GetUserIsInChat(long userId, long chatId)
        {
            var chatAccounts = await _context.FindAsync<IEnumerable<ChatAccount>>();

            bool found = false;

            if (chatAccounts != null)
            {
                foreach (ChatAccount account in chatAccounts)
                {
                    if (account.ChatId == chatId && account.UserId == userId)
                    {
                        found = true;
                    }
                }
            }

            return found;
        }
    }
}
