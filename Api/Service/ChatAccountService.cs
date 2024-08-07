using CSChatLogger.Api;
using CSChatLogger.Schema;
using CSChatLogger.UseCase;

namespace CSChatLogger.Persistence
{
    public class ChatAccountService(Context context) : ContextService(context), IChatAccount
    {
        public void CreateChatAccount(Guid token, long chatId, CreateChatAccountInput dto)
        {
            throw new NotImplementedException();
        }

        public void DeleteChatAccount(Guid token, long chatId)
        {
            throw new NotImplementedException();
        }

        public ReadChatAccountsOutput ReadChatAccounts(Guid token, long chatId)
        {
            throw new NotImplementedException();
        }
    }
}
