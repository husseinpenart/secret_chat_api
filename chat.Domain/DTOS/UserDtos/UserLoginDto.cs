using secre_chat_api.chat.Application.Dictionary;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace secre_chat_api.chat.Domain.DTOS.UserDtos
{
    public class UserLoginDto
    {
        [DefaultValue("09039414838")]
        public string phoneNumber { get; set; }
        [DefaultValue("12345678")]
        public string Password { get; set; }
    }
}
