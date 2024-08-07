using CSChatLogger.Schema;

namespace CSChatLogger.UseCase;

public interface IChatMessage
{
	public void CreateChatMessage(Guid? token, long chatId, SendChatMessageInput dto);

	public Task<ReadChatMessagesOutput> ReadChatMessages(Guid? token, long chatId);

	public void DeleteChatMessage(Guid? token, long chatId, long messageId);
}
