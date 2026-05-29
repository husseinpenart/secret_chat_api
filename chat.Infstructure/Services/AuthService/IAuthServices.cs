using secre_chat_api.chat.Domain.DTOS.UserDtos;

namespace secre_chat_api.chat.Infstructure.Services.AuthService
{
    public interface IAuthServices
    {
        Task<(bool success, string message, UserRegisterDto? Data)> RegisterUserAsync(UserRegisterDto dto);
        //Task<(bool success , string message , string Token)>LoginAsync(UserLoginDto dto);
    }
}
