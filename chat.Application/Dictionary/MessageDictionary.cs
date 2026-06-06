namespace secre_chat_api.chat.Application.Dictionary
{

    /// <summary>
    /// Centralized message dictionary for the application.
    /// Use this for all success, error, validation, and informational messages.
    /// </summary>
    public static class MessageDictionary
    {
        public static class Uservalidation
        {
            public const string PhoneRequirement = "شماره تماس الزامی است";
            public const string identityRequirement = "نام و نام خانوادگی الزامی است";
            public const string userNameRequirement = "نام کاربری الزامی است";
            public const string passwordRequirement = "کلمه عبور الزامی است ";
            public const string AllfieldRequirement = "پر کردن تمامی فیلد ها الزامی است";
            public const string PasswordLengthMessage = "پسور باید حداقل 8 کاراکتر باشد";
            public const string PhoneNumberLength = "شماره موبایل باید 11 رقم باشد ";
            public const string PhoneStartNumber = "شماره موبایل باید با 09  شروع شود";
            public const string UserExsitence = "شما از قبل ثبت نام کرده اید  لطفا وارد شوید";
            public static string SuccessMessage(string label) => $"کاربر با نام  {label} با موفقیت ثبت شد ";
            public static string ExceptionMessage(string label) => $"خطای ثبت کاربر  : {label}";
            // Inside MessageDictionary.Uservalidation
            public static string LockedDownMessage(int minutesLeft) =>
                $"اکانت شما قفل شد. لطفا بعد از {minutesLeft} دقیقه {(minutesLeft > 1 ? "s" : "")} دوباره تلاش کنید.";

        }

        public static class ErrorExceptions
        {
            public const string StatusCode400Error = "unHandled Input Error";
        }
    }
}
