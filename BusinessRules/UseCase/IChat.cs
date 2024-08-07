using CSChatLogger.Schema;

namespace CSChatLogger.UseCase;

public interface IChat
{
	public void CreateChat(Guid? token, CreateChatInput dto);
}
