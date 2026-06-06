using secre_chat_api.chat.Application.Dictionary;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.Entities
{

    [Table("Users")]
    public class UserEntity
    {
        [Key]
        public Guid userId { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = MessageDictionary.Uservalidation.PhoneRequirement)]
        public string phoneNumber { get; set; }
        [Required(ErrorMessage = MessageDictionary.Uservalidation.identityRequirement)]
        public string IdentityName { get; set; }
        [Required(ErrorMessage = MessageDictionary.Uservalidation.userNameRequirement)]
        public string userName { get; set; }
        [Required(ErrorMessage = MessageDictionary.Uservalidation.passwordRequirement)]
        public string Password { get; set; }

        public DateTime RegistredDate { get; set; } = DateTime.UtcNow;
        public int FaildLoginAttempts { get; set; } = 0;
        public DateTime? LockoutUntil { get; set; } = null;
    }
}
