using Microsoft.IdentityModel.Tokens;
using secre_chat_api.chat.Application.Dictionary;
using secre_chat_api.chat.Application.MiddleWares;
using secre_chat_api.chat.Domain.DTOS.UserDtos;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                    return (false, MessageDictionary.Uservalidation.AllfieldRequirement, null);

                var existUser = await _authRepository.GetUserByPhoneAsync(dto.phoneNumber);
                if (existUser != null)
                    return (false, MessageDictionary.Uservalidation.UserExsitence, null);

                var newUser = new UserEntity
                {
                    UserId = Guid.NewGuid(),
                    IdentityName = dto.IdentityName,
                    PhoneNumber = dto.phoneNumber,
                    UserName = dto.userName,
                    Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    RegisteredDate = dto.RegistredDate
                };

                await _authRepository.AddUserAsync(newUser);

                var response = new UserRegisterDto
                {
                    IdentityName = dto.IdentityName,
                    phoneNumber = dto.phoneNumber,
                };

                return (true, MessageDictionary.Uservalidation.SuccessMessage(dto.IdentityName), response); // was: null
            }
            catch (Exception ex)
            {
                return (false, MessageDictionary.Uservalidation.ExceptionMessage(ex.Message), null);
            }
        }

        public async Task<(bool success, string message, string Token)> LoginAsync(UserLoginDto dto)
        {
            try
            {
                var user = await _authRepository.GetUserByPhoneAsync(dto.phoneNumber);
                if (user == null)
                    return (false, MessageDictionary.Uservalidation.AllfieldRequirement, null!);

                if (user.LockoutUntil.HasValue && user.LockoutUntil.Value > DateTime.UtcNow)
                {
                    var minutesLeft = (int)Math.Ceiling((user.LockoutUntil.Value - DateTime.UtcNow).TotalMinutes);
                    return (false, MessageDictionary.Uservalidation.LockedDownMessage(minutesLeft), null!);
                }

                if (BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                {
                    user.FailedLoginAttempts = 0;
                    user.LockoutUntil = null;
                    await _authRepository.UpdateUserAsync(user);

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.IdentityName.ToString()),
                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber.ToString()),
                    };
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddHours(1),
                        Issuer = _configuration["Jwt:Issuer"],
                        Audience = _configuration["Jwt:Audience"],
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return (true, MessageDictionary.Uservalidation.LoginSuccess(user.IdentityName), tokenHandler.WriteToken(token));
                }

                user.FailedLoginAttempts += 1;
                if (user.FailedLoginAttempts >= 3)
                {
                    user.LockoutUntil = DateTime.UtcNow.AddMinutes(15);
                    await _authRepository.UpdateUserAsync(user);
                    return (false, MessageDictionary.Uservalidation.TooManyAttempt, null!);
                }
                else
                {
                    await _authRepository.UpdateUserAsync(user);
                    var attemptsLeft = 3 - user.FailedLoginAttempts;
                    return (false, MessageDictionary.Uservalidation.LockedAttemptLeft(attemptsLeft), null!);
                }
            }
            catch (Exception ex)
            {
                return (false, MessageDictionary.ErrorExceptions.LoginFailed(ex.Message), null!);
            }
        }
    }
}
