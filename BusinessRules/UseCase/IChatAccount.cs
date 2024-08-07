using CSChatLogger.Schema;

namespace CSChatLogger.UseCase;

public interface IChatAccount
{
	public Task<ReadChatAccountsOutput> ReadChatAccounts(Guid? token, long chatId);
	
	public Task CreateChatAccount(Guid? token, long chatId, UpdateChatAccountInput dto);

	public Task DeleteChatAccount(Guid? token, long chatId);
}
