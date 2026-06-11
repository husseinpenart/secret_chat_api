namespace secre_chat_api.chat.Domain.DTOS;

public class ContactRequest
{
    public Guid TargetUserId { get; set; }
    public string? Nickname { get; set; }
}

public class ContactResponse
{
    public Guid OwnerId { get; set; }
    public Guid TargetUserId { get; set; }
    public string TargetUserName { get; set; } = null!;
    public string? Nickname { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime AddedAt { get; set; }
}
