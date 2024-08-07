using CSChatLogger.Api;
using CSChatLogger.Schema;
using CSChatLogger.UseCase;

namespace CSChatLogger.Persistence
{
    public class ChatService(Context context) : ContextService(context), IChat
    {
        public void CreateChat(Guid token, CreateChatInput dto)
        {
            throw new NotImplementedException();
        }
    }
}
