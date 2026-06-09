using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    [Table("Channels")]
    public class ChannelEntity
    {
        [Key]
        public Guid ChannelId { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(OwnerId))]
        public UserEntity Owner { get; set; } = null!;

        public ICollection<ChannelMemberEntity> Members { get; set; } = new List<ChannelMemberEntity>();
        public ICollection<MessageEntity> Messages { get; set; } = new List<MessageEntity>();
    }
}
