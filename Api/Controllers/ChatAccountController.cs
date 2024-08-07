using Microsoft.AspNetCore.Mvc;
using CSChatLogger.Entity;
using CSChatLogger.Schema;

namespace CSChatLogger.Api
{
    [Route("/cschat/1/chat")]
    [ApiController]
    public class ChatAccountController(Context context) : ControllerBase
    {
        private readonly Context _context = context;

        [HttpGet("{chat_id}/account")]
        public async Task<ActionResult<ReadChatAccountsOutput>> ReadChatAccounts(Guid? token, long chat_id)
        {
            if (token == null)
                return Unauthorized();

            long userId = GetUserId((Guid)token);

            if (!GetUserIsInChat(userId, chat_id).Result)
                return Unauthorized();

            var chatAccounts = await _context.FindAsync<IEnumerable<ChatAccount>>();

            ReadChatAccountsOutput output = new ReadChatAccountsOutput();
            output.accounts = [];
            if (chatAccounts != null)
            {
                foreach (ChatAccount account in chatAccounts)
                {
                    if (account.ChatId == chat_id)
                        output.accounts.Add(account.UserId);
                }
            }

            return output;
        }

        [HttpPut("{chat_id}/account")]
        public async Task<IActionResult> AddNewUserToChat(Guid? token, long chat_id, CreateChatAccountInput dto)
        {
            if (token == null)
                return Unauthorized();

            long userId = GetUserId((Guid)token);

            if (!GetUserIsInChat(userId, chat_id).Result)
                return Unauthorized();

            if (GetUserIsInChat(dto.account, chat_id).Result)
                return Conflict();

            ChatAccount chatAccount = new();
            chatAccount.UserId = dto.account;
            chatAccount.ChatId = chat_id;
            _context.ChatAccounts.Add(chatAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{chat_id}/account")]
        public async Task<IActionResult> LeaveChat(Guid? token, long chat_id)
        {
            if (token == null)
                return Unauthorized();

            long userId = GetUserId((Guid) token);

            if (!GetUserIsInChat(userId, chat_id).Result)
                return Unauthorized();

            var chatAccounts = await _context.FindAsync<IEnumerable<ChatAccount>>();

            if (chatAccounts != null)
            {
                foreach (ChatAccount account in chatAccounts)
                {
                    if (account.ChatId == chat_id && account.UserId == userId)
                    {
                        _context.ChatAccounts.Remove(account);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            // TODO: Check if any accounts remain in chat
            // TODO: If no accounts remain in chat, delete all messages and chat

            return NoContent();
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

        private static long GetUserId(Guid token)
        {
            // TODO: Implement
            return -1;
        }
    }
}
