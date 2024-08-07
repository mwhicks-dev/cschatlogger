namespace CSChatLogger.Schema;

public class MessageDto
{
	public long id { get; set; }
	public long sender { get; set; }

	public DateTime datetime { get; set; }

    public string? message { get; set; }
}
