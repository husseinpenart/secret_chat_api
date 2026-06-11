using secre_chat_api.chat.Domain.DTOS;

namespace secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries
{
    public interface IChatRepository
    {
        Task<ChatResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<ChatResponse>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
        Task<ChatResponse> AddAsync(ChatRequest request, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    }
}
