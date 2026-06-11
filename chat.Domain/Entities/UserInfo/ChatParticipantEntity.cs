using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    [Table("ChatParticipants")]
    public class ChatParticipantDtos
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(ChatId))]
        public ChatDtos Chat { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; } = null!;
    }
}
