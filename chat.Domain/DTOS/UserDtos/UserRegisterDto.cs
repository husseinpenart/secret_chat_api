using secre_chat_api.chat.Application.Dictionary;
using System.ComponentModel.DataAnnotations;

namespace secre_chat_api.chat.Domain.DTOS.UserDtos
{
    public class UserRegisterDto
    {
        //[Required(ErrorMessage = MessageDictionary.Uservalidation.PhoneRequirement)]
        public string phoneNumber { get; set; }
        //[Required(ErrorMessage = MessageDictionary.Uservalidation.identityRequirement)]
        public string IdentityName { get; set; }
        //[Required(ErrorMessage = MessageDictionary.Uservalidation.userNameRequirement)]
        public string userName { get; set; }
        //[Required(ErrorMessage = MessageDictionary.Uservalidation.passwordRequirement)]
        public string Password { get; set; }
        public DateTime RegistredDate { get; set; } = DateTime.UtcNow;
    }
}
