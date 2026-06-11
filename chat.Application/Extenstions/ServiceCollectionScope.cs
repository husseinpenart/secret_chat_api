using secre_chat_api.chat.Application.Services.AuthService;
using secre_chat_api.chat.Infrastructure.Persistence.Repositories;
using secre_chat_api.chat.Infstructure.Repository.AuthRepository;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Application.Extenstions
{
    public static class ServiceCollectionScope
    {
        public static IServiceCollection AddSecretChatServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IChannelMemberRepository, ChannelMemberRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            return services;
        }
    }
}
