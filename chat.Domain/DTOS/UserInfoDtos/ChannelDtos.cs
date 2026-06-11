namespace secre_chat_api.chat.Domain.DTOS;

public class ChannelRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Guid OwnerId { get; set; }
}

public class ChannelResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int MemberCount { get; set; }
}
