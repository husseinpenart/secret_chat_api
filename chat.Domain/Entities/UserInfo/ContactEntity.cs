using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    [Table("Contacts")]
    public class ContactEntity
    {
        [Key]
        public Guid ContactId { get; set; } = Guid.NewGuid();

        public Guid OwnerId { get; set; }
        public Guid TargetUserId { get; set; }

        public string? Nickname { get; set; }
        public bool IsBlocked { get; set; } = false;
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(OwnerId))]
        public UserEntity Owner { get; set; } = null!;

        [ForeignKey(nameof(TargetUserId))]
        public UserEntity TargetUser { get; set; } = null!;
    }
}
