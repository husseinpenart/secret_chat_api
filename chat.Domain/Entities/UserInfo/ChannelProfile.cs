using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    [Table("ChannelProfiles")]
    public class ChannelProfile
    {
        [Key]
        public Guid ChannelProfileId { get; set; } = Guid.NewGuid();

        public Guid ChannelId { get; set; }

        [Required, MaxLength(100)]
        public string DisplayName { get; set; } = null!;

        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; } = false;  // which profile is currently active
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(ChannelId))]
        public ChannelEntity Channel { get; set; } = null!;
    }
}
