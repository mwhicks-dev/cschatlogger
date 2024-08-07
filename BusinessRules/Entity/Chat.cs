using System.ComponentModel.DataAnnotations;

namespace CSChatLogger.Entity;

public class Chat
{
    [Key]
    public long Id { get; set; }
}
