using FluentValidation;
using secre_chat_api.chat.Application.Dictionary;
using secre_chat_api.chat.Domain.DTOS.UserDtos;

namespace secre_chat_api.chat.Application.Validator.Authentication
{
    public class LoginValidator : AbstractValidator<UserLoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.phoneNumber)
                   .NotEmpty().WithMessage(MessageDictionary.Uservalidation.PhoneRequirement.ToString())
                   .Length(11).WithMessage(MessageDictionary.Uservalidation.PhoneNumberLength.ToString())
                   .Must(phone => phone != null && phone.StartsWith("09"))
                   .WithMessage(MessageDictionary.Uservalidation.PhoneStartNumber);
            RuleFor(x => x.Password)
                   .NotEmpty().WithMessage(MessageDictionary.Uservalidation.passwordRequirement.ToString())
                   .MinimumLength(8).WithMessage(MessageDictionary.Uservalidation.PasswordLengthMessage.ToString());
        }
    }
}
