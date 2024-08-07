using CSChatLogger.Api;
using CSChatLogger.Entity;
using CSChatLogger.Schema;
using CSChatLogger.UseCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSChatLogger.Persistence
{
    public class ChatService(Context context) : ContextService(context), IChat
    {
        public async Task CreateChat(Guid? token, CreateChatInput dto)
        {
            // Token validation
            long accountId = ValidateAuthorization(token);

            // Input validation
            if (dto.accounts == null || dto.accounts.Count == 0 || 
                (dto.accounts.Count == 1 && dto.accounts[0] == accountId))
            {
                throw new BadRequestException();
            }

            // Implementation
            Chat chat = new();
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            foreach (long account in dto.accounts)
                await CreateChatAccount(chat.Id, account);

            if (!dto.accounts.Contains(accountId))
                await CreateChatAccount(chat.Id, accountId);
        }

        public async Task<ChatsDto> GetChats(Guid? token)
        {
            // Token validation
            long accountId = ValidateAuthorization(token);

            // Implementation
            var chatAccounts = await _context.ChatAccounts.Where(e => e.UserId == accountId).ToListAsync();

            var output = new ChatsDto
            {
                ids = []
            };
            foreach (var chatAccount in chatAccounts) {
                output.ids.Add(chatAccount.ChatId);
            }

            return output;
        }

        private async Task CreateChatAccount(long chatId, long userId)
        {
            ChatAccount chatAccount = new();
            chatAccount.ChatId = chatId;
            chatAccount.UserId = userId;
            _context.ChatAccounts.Add(chatAccount);
            await _context.SaveChangesAsync();
        }
    }
}
