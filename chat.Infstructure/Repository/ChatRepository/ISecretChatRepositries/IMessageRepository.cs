using secre_chat_api.chat.Domain.DTOS;

namespace secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries
{
    public interface IMessageRepository
    {
        Task<MessageResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<MessageResponse>> GetByChatIdAsync(Guid chatId, int skip = 0, int take = 50, CancellationToken ct = default);
        Task<IEnumerable<MessageResponse>> GetByChannelIdAsync(Guid channelId, int skip = 0, int take = 50, CancellationToken ct = default);
        Task<MessageResponse> AddAsync(MessageRequest request, CancellationToken ct = default);
        Task SoftDeleteAsync(Guid id, CancellationToken ct = default);
    }
}
