namespace secre_chat_api.chat.Domain.DTOS;

public class MessageRequest
{
    public Guid SenderId { get; set; }
    public Guid? ChatId { get; set; }
    public Guid? ChannelId { get; set; }
    public string Content { get; set; } = null!;
}

public class MessageResponse
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public string SenderName { get; set; } = null!;
    public Guid? ChatId { get; set; }
    public Guid? ChannelId { get; set; }
    public string Content { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public DateTime SentAt { get; set; }
}
