using CSChatLogger.Api;
using CSChatLogger.Entity;
using CSChatLogger.Schema;
using CSChatLogger.UseCase;
using Microsoft.EntityFrameworkCore;

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
            long accountId = ValidateAuthorization(token, chatId);

            // Implementation
            var chatAccounts = await _context.ChatAccounts.Where(e => e.ChatId == chatId).ToListAsync();

            ReadChatAccountsOutput output = new()
            {
                accounts = []
            };
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
            var chatAccount = await _context.ChatAccounts.FindAsync(accountId, chatId) ?? throw new NotFoundException();

            _context.ChatAccounts.Remove(chatAccount);
            await _context.SaveChangesAsync();

            var chatAccounts = await _context.ChatAccounts.Where(e => e.ChatId == chatId).ToListAsync();
            if (chatAccounts == null || chatAccounts.Count == 0)
            {
                await _context.ChatAccounts.Where(e => e.ChatId == chatId).ExecuteDeleteAsync();
                await _context.ChatMessages.Where(e => e.ChatId == chatId).ExecuteDeleteAsync();
                await _context.Chats.Where(e => e.Id == chatId).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
            }
        }
    }
}
