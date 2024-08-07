namespace CSChatLogger.Entity;

public class ChatAccount
{

	private long chatId;

	private long userId;

	public ChatAccount() {}

	public ChatAccount(long chatId, long userId) 
	{
		SetChatId(chatId);
		SetUserId(userId);
	}

	public long GetChatId() { return chatId; }

	public long GetUserId() { return userId; }

	public void SetChatId(long chatId) { this.chatId = chatId; }

	public void SetUserId(long userId) { this.userId = userId; }
}
