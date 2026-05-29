using secre_chat_api.chat.Application.Dictionary;
using secre_chat_api.chat.Application.MiddleWares;
using secre_chat_api.chat.Domain.DTOS.UserDtos;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.AuthRepository;
using System.Security.Cryptography.X509Certificates;

namespace secre_chat_api.chat.Infstructure.Services.AuthService
{
    public class AuthServices : IAuthServices
    {
        private readonly AppDbContext _context;
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthServices(AppDbContext context, IAuthRepository authRepository, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<(bool success, string message, UserRegisterDto? Data)> RegisterUserAsync(UserRegisterDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return (false, MessageDictionary.Uservalidation.AllfieldRequirement, null);
                }
                switch (true)
                {
                    case var _ when string.IsNullOrWhiteSpace(dto.IdentityName):
                        return (false, MessageDictionary.Uservalidation.identityRequirement, null);
                    case var _ when string.IsNullOrWhiteSpace(dto.phoneNumber):
                        return (false, MessageDictionary.Uservalidation.PhoneRequirement, null);
                    case var _ when string.IsNullOrWhiteSpace(dto.userName):
                        return (false, MessageDictionary.Uservalidation.userNameRequirement, null);
                    case var _ when string.IsNullOrWhiteSpace(dto.Password):
                        return (false, MessageDictionary.Uservalidation.passwordRequirement, null);
                }
                if (dto.Password.Length < 8)

                { return (false, MessageDictionary.Uservalidation.PasswordLengthMessage, null); }
                else if (dto.phoneNumber.Length < 11 || dto.phoneNumber.Length > 11)
                {
                    return (false, MessageDictionary.Uservalidation.PhoneNumberLength, null);
                }
                else if (!dto.phoneNumber.StartsWith("09"))
                {
                    return (false, MessageDictionary.Uservalidation.PhoneStartNumber, null);
                }
                // check user existence
                var ExistUser = await _authRepository.GetUserByPhoneAsync(dto.phoneNumber);
                if (ExistUser != null) return (false, MessageDictionary.Uservalidation.UserExsitence, null);
                var newUser = new UserEntity
                {
                    userId = Guid.NewGuid(),
                    IdentityName = dto.IdentityName,
                    phoneNumber = dto.phoneNumber,
                    userName = dto.userName,
                    Password = dto.Password,
                    RegistredDate = dto.RegistredDate

                };
                await _authRepository.AddUserAsync(newUser);
                var response = new UserRegisterDto
                {
                    IdentityName = dto.IdentityName,
                    phoneNumber = dto.phoneNumber,
                };
                return (true, MessageDictionary.Uservalidation.SuccessMessage(dto.IdentityName), null);

            }
            catch (Exception ex)
            {
                return (true, MessageDictionary.Uservalidation.ExceptionMessage(ex.Message), null);
            }
        }

    }
}
