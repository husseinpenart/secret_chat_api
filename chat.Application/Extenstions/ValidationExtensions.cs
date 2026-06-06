using FluentValidation.Results;
using secre_chat_api.chat.Application.Dictionary;

namespace secre_chat_api.chat.Application.Extenstions
{
    public static class ValidationExtensions
    {
        public static ApiResponseExtention<T> ToCustomErrorResponse<T>(this ValidationResult result)
        {
            return new ApiResponseExtention<T>
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = MessageDictionary.ErrorExceptions.StatusCode400Error, // General "Validation Failed" message
                Errors = result.Errors.Select(e => e.ErrorMessage).ToList(), // Individual messages
                Data = default
            };
        }
    }
}
