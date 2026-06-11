using secre_chat_api.chat.Domain.DTOS;

namespace secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries
{
    public interface IChannelMemberRepository
    {
        Task<ChannelMemberResponse?> GetByIdAsync(Guid channelId, Guid userId, CancellationToken ct = default);
        Task<IEnumerable<ChannelMemberResponse>> GetByChannelIdAsync(Guid channelId, CancellationToken ct = default);
        Task<IEnumerable<ChannelMemberResponse>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
        Task<ChannelMemberResponse> AddAsync(ChannelMemberRequest request, CancellationToken ct = default);
        Task UpdateAdminStatusAsync(Guid channelId, Guid userId, bool isAdmin, CancellationToken ct = default);
        Task RemoveAsync(Guid channelId, Guid userId, CancellationToken ct = default);
    }
}
