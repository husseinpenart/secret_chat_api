
namespace secre_chat_api.chat.Application.Services.ChannelService
{
    public interface IChannelService
    {
        Task<(bool success, string message, ChannelResponseDto? Data)> CreateChannelAsync(ChannelRequestDto dto, Guid creatorId);
        Task<(bool success, string message, ChannelResponseDto? Data)> GetChannelByIdAsync(Guid channelId);
        Task<(bool success, string message)> AddSubscriberAsync(Guid channelId, Guid userId);
        Task<(bool success, string message)> RemoveSubscriberAsync(Guid channelId, Guid userId, Guid requesterId);
        Task<(bool success, string message)> SetProfileAsync(Guid channelId, ChannelProfileDto dto, Guid requesterId);
        Task<(bool success, string message)> DeleteChannelAsync(Guid channelId, Guid requesterId);
    }
}
