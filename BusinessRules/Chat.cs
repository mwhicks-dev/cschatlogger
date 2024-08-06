namespace CSChatLogger.Entity;

public class Chat
{
    private long id;

    public Chat() { }

    public Chat(long id)
    {
        SetId(id);
    }

    public long GetId() { return id; }

    public void SetId(long id) { this.id = id; }
}
