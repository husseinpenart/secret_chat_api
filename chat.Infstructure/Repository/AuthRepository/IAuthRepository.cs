using secre_chat_api.chat.Domain.Entities;

namespace secre_chat_api.chat.Infstructure.Repository.AuthRepository
{
    public interface IAuthRepository
    {
        Task AddUserAsync(UserEntity user);
        Task<UserEntity> GetUserByPhoneAsync( string phoneNumber);
        Task UpdateUserAsync(UserEntity user);
    }
}
