namespace secre_chat_api.chat.Domain.DTOS.UserInfoDtos.ResponseDto
{
    public class ChatResponse
    {
        public ChatType Type { get; set; } = ChatType.Private;
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ChatParticipantDtos> Participants { get; set; } = new List<ChatParticipantDtos>();
        public ICollection<MessageDtos> Messages { get; set; } = new List<MessageDtos>();
    }
}
