using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSChatLogger.Entity;

[PrimaryKey(nameof(ChatId), nameof(MessageId))]
public class ChatMessage
{
    [ForeignKey("Chat")]
    [Column(Order = 1)]
    public long ChatId { get; set; }

    public long UserId { get; set; }

    [Column(Order = 2)]
    public long MessageId { get; set; }

    public string? Message { get; set; }

    public DateTime DateTime { get; set; }
}
