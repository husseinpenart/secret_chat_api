using secre_chat_api.chat.Application.Dictionary;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{
    [Table("Users")]
    public class UserEntity
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = MessageDictionary.Uservalidation.PhoneRequirement)]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = MessageDictionary.Uservalidation.identityRequirement)]
        public string IdentityName { get; set; } = null!;

        [Required(ErrorMessage = MessageDictionary.Uservalidation.userNameRequirement)]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = MessageDictionary.Uservalidation.passwordRequirement)]
        public string Password { get; set; } = null!;

        public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockoutUntil { get; set; }

        // Navigation properties
        public ICollection<ContactEntity> Contacts { get; set; } = new List<ContactEntity>();
        public ICollection<ChatParticipantEntity> ChatParticipants { get; set; } = new List<ChatParticipantEntity>();
        public ICollection<ChannelMemberEntity> ChannelMembers { get; set; } = new List<ChannelMemberEntity>();
        public ICollection<MessageEntity> SentMessages { get; set; } = new List<MessageEntity>();
    }
}
