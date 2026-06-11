using secre_chat_api.chat.Domain.DTOS;

public interface IChatParticipantRepository
{
    Task<ChatParticipantResponse?> GetByIdAsync(Guid chatId, Guid userId, CancellationToken ct = default);
    Task<IEnumerable<ChatParticipantResponse>> GetByChatIdAsync(Guid chatId, CancellationToken ct = default);
    Task<IEnumerable<ChatParticipantResponse>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task AddAsync(Guid chatId, Guid userId, CancellationToken ct = default);
    Task RemoveAsync(Guid chatId, Guid userId, CancellationToken ct = default);
    Task<bool> ExistsAsync(Guid chatId, Guid userId, CancellationToken ct = default);
}