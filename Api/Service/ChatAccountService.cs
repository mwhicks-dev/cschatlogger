using CSChatLogger.Api;
using CSChatLogger.Entity;
using CSChatLogger.Schema;
using CSChatLogger.UseCase;

namespace CSChatLogger.Persistence
{
    public class ChatAccountService(Context context) : ContextService(context), IChatAccount
    {
        public async Task CreateChatAccount(Guid? token, long chatId, UpdateChatAccountInput dto)
        {
            // Token validation
            ValidateAuthorization(token, chatId);

            // Input validation
            if (GetUserIsInChat(dto.account, chatId).Result)
                throw new ConflictException();

            // Implementation
            ChatAccount chatAccount = new();
            chatAccount.UserId = dto.account;
            chatAccount.ChatId = chatId;
            _context.ChatAccounts.Add(chatAccount);
            await _context.SaveChangesAsync();
        }

        public async Task<ReadChatAccountsOutput> ReadChatAccounts(Guid? token, long chatId)
        {
            // Token validation
            ValidateAuthorization(token, chatId);

            // Implementation
            var chatAccounts = await _context.FindAsync<IEnumerable<ChatAccount>>();

            ReadChatAccountsOutput output = new ReadChatAccountsOutput();
            output.accounts = [];
            if (chatAccounts != null)
            {
                foreach (ChatAccount account in chatAccounts)
                {
                    if (account.ChatId == chatId)
                        output.accounts.Add(account.UserId);
                }
            }

            return output;
        }

        public async Task DeleteChatAccount(Guid? token, long chatId)
        {
            // Token validation
            long accountId = ValidateAuthorization(token, chatId);

            // Implementation
            var chatAccounts = await _context.FindAsync<IEnumerable<ChatAccount>>();

            if (chatAccounts != null)
            {
                foreach (ChatAccount account in chatAccounts)
                {
                    if (account.ChatId == chatId && account.UserId == accountId)
                    {
                        _context.ChatAccounts.Remove(account);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            // TODO: Check if any accounts remain in chat
            // TODO: If no accounts remain in chat, delete all messages and chat
        }
    }
}
