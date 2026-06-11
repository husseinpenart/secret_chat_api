using secre_chat_api.chat.Domain.Entities;

namespace secre_chat_api.chat.Domain.DTOS;

public class ChatRequest
{
    public ChatType Type { get; set; } = ChatType.Private;
    public string? Title { get; set; }
    public List<Guid> ParticipantIds { get; set; } = new();
}

public class ChatResponse
{
    public Guid Id { get; set; }
    public ChatType Type { get; set; }
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ChatParticipantResponse> Participants { get; set; } = new();
}
