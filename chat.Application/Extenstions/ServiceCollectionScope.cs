using secre_chat_api.chat.Application.Services.AuthService;
using secre_chat_api.chat.Application.Services.ChannelMemberService;
using secre_chat_api.chat.Application.Services.ChannelService;
using secre_chat_api.chat.Application.Services.ChatParticipantService;
using secre_chat_api.chat.Application.Services.ChatService;
using secre_chat_api.chat.Application.Services.ContactService;
using secre_chat_api.chat.Application.Services.MessageService;
using secre_chat_api.chat.Infrastructure.Persistence.Repositories;
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
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IChannelService, ChannelService>();
            services.AddScoped<IChannelMemberService, ChannelMemberService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IChatParticipantService, ChatParticipantService>();
            services.AddScoped<IContactService, ContactService>();


            return services;
        }
    }
}
