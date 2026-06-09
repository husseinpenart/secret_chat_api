using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using secre_chat_api.chat.Application.Dictionary;
using secre_chat_api.chat.Application.Extenstions;
using secre_chat_api.chat.Application.Services.AuthService;
using secre_chat_api.chat.Domain.DTOS.UserDtos;

namespace secre_chat_api.chat.API.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly IValidator<UserRegisterDto> _validator;
        private readonly IValidator<UserLoginDto> _loginValidator;
        public AuthController(IAuthServices authServices, IValidator<UserRegisterDto> validator, IValidator<UserLoginDto> loginValidator)
        {
            _authServices = authServices;
            _validator = validator;
            _loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(
                    new ApiResponseExtention<UserRegisterDto>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = MessageDictionary.ErrorExceptions.StatusCode400Error,
                        Data = null
                    }
                );
            /// <summary>
            /// Fluent Validaotion called in this part
            /// </summary>
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToCustomErrorResponse<UserRegisterDto>());
            }
            ///    /// <summary>
            /// Fluent Validaotion called in this part
            /// </summary>

            var result = await _authServices.RegisterUserAsync(dto);
            if (!result.success)
                return BadRequest(
                    new ApiResponseExtention<UserRegisterDto>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = result.message,
                        Data = null
                    }
                );
            return Ok(
                new ApiResponseExtention<UserRegisterDto>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = result.message,
                    Data = result.Data
                });

        }
        /// login controller
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    new ApiResponseExtention<UserLoginDto>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = MessageDictionary.ErrorExceptions.StatusCode400Error,
                        Data = null
                    }
                );
            /// <summary>
            /// Fluent Validaotion called in this part
            /// </summary>
            var validationResult = await _loginValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToCustomErrorResponse<UserLoginDto>());
            }
            ///    /// <summary>
            /// Fluent Validaotion called in this part
            /// </summary>

            var result = await _authServices.LoginAsync(dto);
            if (!result.success) return BadRequest(
               new ApiResponseExtention<string>

               {
                   Success = false,
                   StatusCode = StatusCodes.Status400BadRequest,
                   Message = result.message,
                   Data = null,
               }
               );
            return Ok(
                new ApiResponseExtention<string>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = result.message,
                    Data = result.Token,
                });
        }
    }
}
