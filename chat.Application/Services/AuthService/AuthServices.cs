using secre_chat_api.chat.Application.Dictionary;
using secre_chat_api.chat.Application.MiddleWares;
using secre_chat_api.chat.Domain.DTOS.UserDtos;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.AuthRepository;
using System.Security.Cryptography.X509Certificates;

namespace secre_chat_api.chat.Application.Services.AuthService
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


                // check user existence
                var ExistUser = await _authRepository.GetUserByPhoneAsync(dto.phoneNumber);
                if (ExistUser != null) return (false, MessageDictionary.Uservalidation.UserExsitence, null);
                var newUser = new UserEntity
                {
                    userId = Guid.NewGuid(),
                    IdentityName = dto.IdentityName,
                    phoneNumber = dto.phoneNumber,
                    userName = dto.userName,
                    Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
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
                return (false, MessageDictionary.Uservalidation.ExceptionMessage(ex.Message), null);
            }
        }
        /// <summary>
          // Login section
        public async Task<(bool success, string message, string Token)> LoginAsync(UserLoginDto dto)
        {
            try
            {
                var user = await _authRepository.GetUserByPhoneAsync(dto.phoneNumber);

                if (user != null) return (false, MessageDictionary.Uservalidation.AllfieldRequirement, null);

                ///Check if locked out
                if (user.LockoutUntil.HasValue && user.LockoutUntil.Value > DateTime.UtcNow) // Changed < to >
                {
                    // Math.Ceiling returns double, we cast to int for the message
                    var minutesLeft = (int)Math.Ceiling((user.LockoutUntil.Value - DateTime.UtcNow).TotalMinutes);

                    // Pass it as an int (or .ToString() if your method strictly takes string)
                    return (false, MessageDictionary.Uservalidation.LockedDownMessage(minutesLeft), null);
                }

                if(BCrypt.Net.BCrypt.Verify(dto.Password , user.Password))
                {
                    user.FaildLoginAttempts = 0;
                    user.LockoutUntil = null;
                    await _authRepository.UpdateUserAsync(user);
                    var tokenHandler = new JwtS
                }


            }
            }

        /// </summary>
    }
}
