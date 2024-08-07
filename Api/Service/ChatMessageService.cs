using CSChatLogger.Api;
using CSChatLogger.Schema;
using CSChatLogger.UseCase;

namespace CSChatLogger.Persistence
{
    public class ChatMessageService(Context context) : ContextService(context), IChatMessage
    {
        public void CreateChatMessage(Guid token, long chatId, SendChatMessageInput dto)
        {
            throw new NotImplementedException();
        }

        public void DeleteChatMessage(Guid token, long chatId, long messageId)
        {
            throw new NotImplementedException();
        }

        public ReadChatMessagesOutput ReadChatMessages(Guid token, long chatId)
        {
            throw new NotImplementedException();
        }
    }
}