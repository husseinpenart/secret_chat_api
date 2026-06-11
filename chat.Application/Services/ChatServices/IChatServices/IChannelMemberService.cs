using secre_chat_api.chat.Domain.DTOS.ChannelDtos;

namespace secre_chat_api.chat.Application.Services.ChannelMemberService
{
    public interface IChannelMemberService
    {
        Task<(bool success, string message)> AddMemberAsync(Guid channelId, Guid userId, Guid requesterId);
        Task<(bool success, string message)> RemoveMemberAsync(Guid channelId, Guid userId, Guid requesterId);
        Task<(bool success, string message)> UpdateAdminStatusAsync(Guid channelId, Guid userId, bool isAdmin, Guid requesterId);
        Task<(bool success, string message, IEnumerable<ChannelMemberResponseDto>? Data)> GetMembersAsync(Guid channelId);
    }
}
