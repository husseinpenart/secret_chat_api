using secre_chat_api.chat.Domain.DTOS.UserDtos;
using secre_chat_api.chat.Domain.Entities;

namespace secre_chat_api.chat.Application.Services.UserService
{
    public interface IUserService
    {
        Task<(bool success, string message, UserResponse? Data)> GetUserByIdAsync(Guid userId);
        Task<(bool success, string message)> SetProfileAsync(Guid userId, UserProfile dto);
        Task<(bool success, string message)> UpdateUserAsync(Guid userId, UserProfileResponse dto);
        Task<(bool success, string message)> DeleteUserAsync(Guid userId);
    }
}
