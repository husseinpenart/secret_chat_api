using secre_chat_api.chat.Application.Dictionary;
using System.ComponentModel.DataAnnotations;

namespace secre_chat_api.chat.Domain.DTOS.UserDtos
{
    public class UserRegisterDto
    {
        public string phoneNumber { get; set; }
        public string IdentityName { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public DateTime RegistredDate { get; set; } = DateTime.UtcNow;
    }
}
