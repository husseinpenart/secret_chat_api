using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    public enum MessageType { Text, Image, Video, File, Audio }

    [Table("Messages")]
    public class MessageEntity
    {
        [Key]
        public Guid MessageId { get; set; } = Guid.NewGuid();

        public Guid SenderId { get; set; }
        public Guid? ChatId { get; set; }
        public Guid? ChannelId { get; set; }
        public Guid? ReplyToMessageId { get; set; }

        public MessageType Type { get; set; } = MessageType.Text;

        // nullable — media-only messages may have no text
        public string? Content { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public DateTime? EditedAt { get; set; }

        [ForeignKey(nameof(SenderId))]
        public UserEntity Sender { get; set; } = null!;

        [ForeignKey(nameof(ChatId))]
        public ChatEntity? Chat { get; set; }

        [ForeignKey(nameof(ChannelId))]
        public ChannelEntity? Channel { get; set; }

        [ForeignKey(nameof(ReplyToMessageId))]
        public MessageEntity? ReplyTo { get; set; }

        public ICollection<MediaAttachment> Attachments { get; set; } = new List<MediaAttachment>();
    }
}
