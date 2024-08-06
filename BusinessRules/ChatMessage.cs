namespace CSChatLogger.Entity;

public class ChatMessage
{

	private long chatId;

	private long userId;

	private long messageId;

	private string? message;

	private DateTime dateTime;

	public ChatMessage() { }

	public ChatMessage(long chatId, long userId, long messageId, string message, DateTime dateTime)
    {
        SetChatId(chatId);
        SetUserId(userId);
        SetMessageId(messageId);
        SetMessage(message);
        SetDateTime(dateTime);
    }

    public long GetChatId() { return chatId; }

    public long GetUserId() { return userId; }

    public long GetMessageId() { return messageId; }

    public string GetMessage() { return message; }

    public DateTime GetDateTime() { return dateTime; }

    public void SetChatId(long chatId) { this.chatId = chatId; }

    public void SetUserId(long userId) { this.userId = userId; }

    public void SetMessageId(long messageId) { this.messageId = messageId; }

    public void SetMessage(string message) { this.message = message; }

    public void SetDateTime(DateTime dateTime) { this.dateTime = dateTime; }
}
