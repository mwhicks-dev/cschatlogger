using CSChatLogger.Entity;
using Microsoft.EntityFrameworkCore;

namespace CSChatLogger.Api
{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {
        public DbSet<Chat> Chats { get; set; } = null!;

        public DbSet<ChatAccount> ChatAccounts { get; set; } = null!;

        public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
    }
}
