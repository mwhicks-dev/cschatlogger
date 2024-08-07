using CSChatLogger.Schema;

namespace CSChatLogger.UseCase;

public interface IChatMessage
{
	public Task CreateChatMessage(Guid? token, long chatId, SendChatMessageInput dto);

	public Task<ReadChatMessagesOutput> ReadChatMessages(Guid? token, long chatId);

	public Task UpdateChatMessage(Guid? token, long chatId, long messageId, UpdateChatMessageInput dto);

	public Task DeleteChatMessage(Guid? token, long chatId, long messageId);
}
