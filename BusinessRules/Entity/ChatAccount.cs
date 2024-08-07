using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSChatLogger.Entity;

[PrimaryKey(nameof(UserId), nameof(ChatId))]
public class ChatAccount
{
    [Column(Order = 0)]
    public long UserId { get; set; }
    
    [ForeignKey("Chat")]
    [Column(Order = 1)]
    public long ChatId { get; set; }
}
