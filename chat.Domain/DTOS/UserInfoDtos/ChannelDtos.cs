namespace secre_chat_api.chat.Domain.DTOS;

public class ChannelRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    // OwnerId comes from auth token, not request body — removed
}

public class ChannelResponse
{
    public Guid ChannelId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int MemberCount { get; set; }
    public ChannelProfileResponse? ActiveProfile { get; set; }
}

public class ChannelProfileRequest
{
    public string DisplayName { get; set; } = null!;
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
}

public class ChannelProfileResponse
{
    public Guid ChannelProfileId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
