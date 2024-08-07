using CSChatLogger.Schema;

namespace CSChatLogger.UseCase;

public interface IChat
{
	public Task CreateChat(Guid? token, CreateChatInput dto);
}
