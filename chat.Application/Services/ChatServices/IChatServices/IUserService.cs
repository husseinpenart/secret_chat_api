using secre_chat_api.chat.Domain.DTOS.UserDtos;

namespace secre_chat_api.chat.Application.Services.UserService
{
    public interface IUserService
    {
        Task<(bool success, string message, UserResponseDto? Data)> GetUserByIdAsync(Guid userId);
        Task<(bool success, string message)> SetProfileAsync(Guid userId, UserProfileDto dto);
        Task<(bool success, string message)> UpdateUserAsync(Guid userId, UserUpdateDto dto);
        Task<(bool success, string message)> DeleteUserAsync(Guid userId);
    }
}
