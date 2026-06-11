using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    public enum ChatType { Private, Group }

    [Table("Chats")]
    public class ChatDtos
    {
        [Key]
        public Guid ChatId { get; set; } = Guid.NewGuid();

        public ChatType Type { get; set; } = ChatType.Private;
        public string? Title { get; set; }       
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ChatParticipantDtos> Participants { get; set; } = new List<ChatParticipantDtos>();
        public ICollection<MessageDtos> Messages { get; set; } = new List<MessageDtos>();
    }
}
