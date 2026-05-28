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
        }
    }
}
