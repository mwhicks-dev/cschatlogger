using CSChatLogger.Schema;

namespace CSChatLogger.UseCase;

public interface IChatAccount
{
	public Task<ReadChatAccountsOutput> ReadChatAccounts(Guid? token, long chatId);
	
	public void CreateChatAccount(Guid? token, long chatId, UpdateChatAccountInput dto);

	public void DeleteChatAccount(Guid? token, long chatId);
}
