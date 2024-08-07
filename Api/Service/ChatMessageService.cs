using CSChatLogger.Api;
using CSChatLogger.Entity;
using CSChatLogger.Schema;
using CSChatLogger.UseCase;

namespace CSChatLogger.Persistence
{
    public class ChatMessageService(Context context) : ContextService(context), IChatMessage
    {
        public async void CreateChatMessage(Guid? token, long chatId, SendChatMessageInput dto)
        {
            // Token validation
            long accountId = ValidateAuthorization(token, chatId);

            // Implementation
            ChatMessage chatMessage = new();
            chatMessage.UserId = accountId;
            chatMessage.ChatId = chatId;
            chatMessage.Message = dto.message;
            chatMessage.DateTime = DateTime.UtcNow;
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }

        public async void DeleteChatMessage(Guid? token, long chatId, long messageId)
        {
            // Token validation
            long accountId = ValidateAuthorization(token, chatId, messageId);

            // Implementation
            var chatMessages = await _context.FindAsync<IEnumerable<ChatMessage>>();
            if (chatMessages == null)
                throw new NotFoundException();

            ChatMessage? chatMessage = null;
            foreach (ChatMessage tmp in chatMessages)
            {
                if (tmp.MessageId == messageId)
                {
                    if (tmp.UserId != accountId)
                        throw new UnauthorizedException();

                    chatMessage = tmp;
                    break;
                }
            }

            if (chatMessage == null)
                throw new NotFoundException();

            _context.ChatMessages.Remove(chatMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<ReadChatMessagesOutput> ReadChatMessages(Guid? token, long chatId)
        {
            // Token validation
            ValidateAuthorization(token, chatId);

            // Implementation
            var chatMessages = await _context.FindAsync<IEnumerable<ChatMessage>>();

            ReadChatMessagesOutput output = new()
            {
                messages = []
            };

            if (chatMessages != null)
            {
                foreach (ChatMessage message in chatMessages)
                {
                    if (message.ChatId == chatId)
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
    }
}