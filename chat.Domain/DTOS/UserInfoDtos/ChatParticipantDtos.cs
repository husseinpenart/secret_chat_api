namespace secre_chat_api.chat.Domain.DTOS;

public class ChatParticipantResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public DateTime JoinedAt { get; set; }
}
