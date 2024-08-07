using Microsoft.AspNetCore.Mvc;
using CSChatLogger.Schema;
using CSChatLogger.Entity;
using CSChatLogger.Persistence;

namespace CSChatLogger.Api
{
    [Route("/cschat/1/chat")]
    [ApiController]
    public class ChatMessageController(Context context) : ControllerBase
    {
        private readonly Context _context = context;

        private readonly ChatMessageService service = new(context);

        [HttpPost("{chat_id}/message")]
        public IActionResult SendChatMessage(Guid? token, long chat_id, SendChatMessageInput dto)
        {
            try
            {
                service.CreateChatMessage(token, chat_id, dto);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }

            return NoContent();
        }

        [HttpGet("{chat_id}/message")]
        public async Task<ActionResult<ReadChatMessagesOutput>> ReadChatMessages(Guid? token, long chat_id)
        {
            ReadChatMessagesOutput? output;
            try
            {
                output = await service.ReadChatMessages(token, chat_id);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
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
        public IActionResult DeleteChatMessage(Guid? token, long chat_id, long message_id)
        {
            try
            {
                service.DeleteChatMessage(token, chat_id, message_id);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ContextService.NotFoundException)
            {
                return NotFound();
            }

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
