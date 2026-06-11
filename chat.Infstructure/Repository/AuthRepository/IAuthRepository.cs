using secre_chat_api.chat.Domain.DTOS.UserDtos;

namespace secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries
{
    public interface IAuthRepository
    {
        Task<UserResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<UserResponse?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken ct = default);
        Task<UserResponse> RegisterAsync(UserRegisterDto request, CancellationToken ct = default);
        Task<UserProfileResponse> SetProfileAsync(Guid userId, UserProfileRequest request, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    }
}
