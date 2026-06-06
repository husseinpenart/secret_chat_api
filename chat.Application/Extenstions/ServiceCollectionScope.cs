using secre_chat_api.chat.Application.Services.AuthService;
using secre_chat_api.chat.Infstructure.Repository.AuthRepository;

namespace secre_chat_api.chat.Application.Extenstions
{
    public static class ServiceCollectionScope
    {
        public static IServiceCollection AddSecretChatServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IAuthRepository, AuthRepository>();


            return services;
        }
    }
}
