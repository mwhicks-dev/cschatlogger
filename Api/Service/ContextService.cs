using CSChatLogger.Api;
using CSChatLogger.Entity;

namespace CSChatLogger.Persistence
{
    public abstract class ContextService(Context context)
    {

        private readonly Context _context = context;

        protected async Task<bool> GetUserIsInChat(long userId, long chatId)
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

        protected static long GetUserId(Guid token)
        {
            // TODO: Implement
            return -1;
        }
    }
}
