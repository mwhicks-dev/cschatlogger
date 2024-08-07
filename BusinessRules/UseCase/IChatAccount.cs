using CSChatLogger.Schema;

namespace CSChatLogger.UseCase;

public interface IChatAccount
{
	public Task<ReadChatAccountsOutput> ReadChatAccountsAsync(Guid? token, long chatId);
	
	public void CreateChatAccount(Guid? token, long chatId, CreateChatAccountInput dto);

	public void DeleteChatAccount(Guid? token, long chatId);
}
