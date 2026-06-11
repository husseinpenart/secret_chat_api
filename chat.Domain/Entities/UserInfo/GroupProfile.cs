using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    [Table("GroupProfiles")]
    public class GroupProfile
    {
        [Key]
        public Guid GroupProfileId { get; set; } = Guid.NewGuid();

        public Guid ChatId { get; set; }
        public Guid OwnerId { get; set; }

        [Required, MaxLength(100)]
        public string DisplayName { get; set; } = null!;

        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; } = false;  // which profile is currently active
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(ChatId))]
        public ChatEntity Chat { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public UserEntity Owner { get; set; } = null!;
    }
}
