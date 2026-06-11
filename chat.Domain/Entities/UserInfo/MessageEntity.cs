using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    [Table("Messages")]
    public class MessageDtos
    {
        [Key]
        public Guid MessageId { get; set; } = Guid.NewGuid();

        public Guid SenderId { get; set; }
        public Guid? ChatId { get; set; }      
        public Guid? ChannelId { get; set; }    

        [Required]
        public string Content { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(SenderId))]
        public UserEntity Sender { get; set; } = null!;

        [ForeignKey(nameof(ChatId))]
        public ChatDtos? Chat { get; set; }

        [ForeignKey(nameof(ChannelId))]
        public ChannelDtos? Channel { get; set; }
    }
}
