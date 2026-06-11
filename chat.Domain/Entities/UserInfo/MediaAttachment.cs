using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    public enum MediaType { Image, Video, Audio, File }

    [Table("MediaAttachments")]
    public class MediaAttachment
    {
        [Key]
        public Guid AttachmentId { get; set; } = Guid.NewGuid();

        public Guid MessageId { get; set; }
        public MediaType MediaType { get; set; }

        [Required]
        public string Url { get; set; } = null!;

        public string? FileName { get; set; }
        public long? FileSizeBytes { get; set; }
        public string? MimeType { get; set; }

        [ForeignKey(nameof(MessageId))]
        public MessageEntity Message { get; set; } = null!;
    }
}
