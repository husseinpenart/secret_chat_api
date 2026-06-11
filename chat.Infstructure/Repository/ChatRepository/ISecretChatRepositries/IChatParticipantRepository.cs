using secre_chat_api.chat.Domain.DTOS;

namespace secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries
{
    public interface IChatParticipantRepository
    {
        Task<ChatParticipantResponse?> GetByIdAsync(Guid chatId, Guid userId, CancellationToken ct = default);
        Task<IEnumerable<ChatParticipantResponse>> GetByChatIdAsync(Guid chatId, CancellationToken ct = default);
        Task<IEnumerable<ChatParticipantResponse>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
        Task<ChatParticipantResponse> AddAsync(ChatParticipantRequest request, CancellationToken ct = default);
        Task UpdateAdminStatusAsync(Guid chatId, Guid userId, bool isAdmin, CancellationToken ct = default);
        Task RemoveAsync(Guid chatId, Guid userId, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid chatId, Guid userId, CancellationToken ct = default);
    }
}
