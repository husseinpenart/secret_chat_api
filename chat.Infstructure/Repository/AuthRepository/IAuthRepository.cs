using secre_chat_api.chat.Domain.Entities;

public interface IAuthRepository
{
    Task AddUserAsync(UserEntity user);
    Task<UserEntity?> GetUserByPhoneAsync(string phoneNumber);
    Task UpdateUserAsync(UserEntity user);
}
