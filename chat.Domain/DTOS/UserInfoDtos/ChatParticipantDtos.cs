namespace secre_chat_api.chat.Domain.DTOS;

public class ChatParticipantRequest
{
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public bool IsAdmin { get; set; } = false;
}

public class ChatParticipantResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string? DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime JoinedAt { get; set; }
}
