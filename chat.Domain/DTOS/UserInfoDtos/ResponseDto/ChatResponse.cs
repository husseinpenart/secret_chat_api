using secre_chat_api.chat.Domain.Entities;

namespace secre_chat_api.chat.Domain.DTOS.UserInfoDtos.ResponseDto
{
    public class ChatResponse
    {
        public ChatType Type { get; set; } = ChatType.Private;
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ChatParticipantRequest> Participants { get; set; } = new List<ChatParticipantRequest>();
        public ICollection<MessageRequest> Messages { get; set; } = new List<MessageRequest>();
    }
}
