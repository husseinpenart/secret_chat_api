using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    [Table("ChannelMembers")]
    public class ChannelMemberEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ChannelId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(ChannelId))]
        public ChannelEntity Channel { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; } = null!;
    }
}
