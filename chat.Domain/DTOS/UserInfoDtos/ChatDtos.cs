using secre_chat_api.chat.Domain.Entities;

namespace secre_chat_api.chat.Domain.DTOS;

public class ChatRequest
{
    public ChatType Type { get; set; } = ChatType.Private;
    public string? Title { get; set; }
    public List<Guid> ParticipantIds { get; set; } = new();
    // only relevant when Type == Group
    public GroupProfileRequest? GroupProfile { get; set; }
}

public class ChatResponse
{
    public Guid ChatId { get; set; }
    public ChatType Type { get; set; }
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public GroupProfileResponse? ActiveGroupProfile { get; set; }
    public List<ChatParticipantResponse> Participants { get; set; } = new();
}

public class GroupProfileRequest
{
    public string DisplayName { get; set; } = null!;
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
}

public class GroupProfileResponse
{
    public Guid GroupProfileId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
}
