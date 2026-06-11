using secre_chat_api.chat.Domain.DTOS;

namespace secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries
{
    public interface IChannelRepository
    {
        Task<ChannelResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<ChannelResponse>> GetByOwnerIdAsync(Guid ownerId, CancellationToken ct = default);
        Task<IEnumerable<ChannelResponse>> GetAllAsync(CancellationToken ct = default);
        Task<ChannelResponse> AddAsync(ChannelRequest request, CancellationToken ct = default);
        Task UpdateAsync(Guid id, ChannelRequest request, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    }
}
