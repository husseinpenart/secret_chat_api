namespace secre_chat_api.chat.Domain.DTOS;

public class ContactRequest
{
    public Guid TargetUserId { get; set; }
    public string? Nickname { get; set; }
}

public class BlockContactRequest
{
    public Guid TargetUserId { get; set; }
    public bool IsBlocked { get; set; }
}

public class ContactResponse
{
    public Guid ContactId { get; set; }
    public Guid OwnerId { get; set; }
    public Guid TargetUserId { get; set; }
    public string TargetUserName { get; set; } = null!;
    public string? TargetDisplayName { get; set; }
    public string? TargetAvatarUrl { get; set; }
    public string? Nickname { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime AddedAt { get; set; }
}
