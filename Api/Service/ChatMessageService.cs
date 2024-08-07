using CSChatLogger.Api;
using CSChatLogger.Entity;
using CSChatLogger.Schema;
using CSChatLogger.UseCase;
using Microsoft.EntityFrameworkCore;

namespace CSChatLogger.Persistence
{
    public class ChatMessageService(Context context) : ContextService(context), IChatMessage
    {
        public async Task CreateChatMessage(Guid? token, long chatId, SendChatMessageInput dto)
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

        public async Task DeleteChatMessage(Guid? token, long chatId, long messageId)
        {
            // Token validation
            long accountId = ValidateAuthorization(token, chatId, messageId);

            // Implementation
            var chatMessage = await _context.ChatMessages.FindAsync(chatId, messageId) ?? throw new NotFoundException();

            if (chatMessage.UserId != accountId)
                throw new UnauthorizedException();

            _context.ChatMessages.Remove(chatMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<ReadChatMessagesOutput> ReadChatMessages(Guid? token, long chatId)
        {
            // Token validation
            ValidateAuthorization(token, chatId);

            // Implementation
            var chatMessages = await _context.ChatMessages.Where(e => e.ChatId == chatId).ToListAsync();

            ReadChatMessagesOutput output = new()
            {
                messages = []
            };

            if (chatMessages != null)
            {
                foreach (ChatMessage message in chatMessages)
                {
                    MessageDto messageDto = new()
                    {
                        sender = message.UserId,
                        datetime = message.DateTime,
                        id = message.MessageId,
                        message = message.Message
                    };
                    output.messages.Add(messageDto);
                }
            }

            return output;
        }

        public async Task UpdateChatMessage(Guid? token, long chatId, long messageId, UpdateChatMessageInput dto)
        {
            // Token validation
            long accountId = ValidateAuthorization(token, chatId);

            // Implementation
            var chatMessage = await _context.ChatMessages.FindAsync(chatId, messageId) ?? throw new NotFoundException();

            chatMessage.Message = dto.message;
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }
    }
}