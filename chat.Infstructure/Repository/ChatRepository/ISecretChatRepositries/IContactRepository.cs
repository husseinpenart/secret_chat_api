using secre_chat_api.chat.Domain.DTOS;

namespace secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries
{
    public interface IContactRepository
    {
        Task<ContactResponse?> GetByIdAsync(Guid ownerId, Guid targetUserId, CancellationToken ct = default);
        Task<IEnumerable<ContactResponse>> GetByOwnerIdAsync(Guid ownerId, CancellationToken ct = default);
        Task<ContactResponse> AddAsync(ContactRequest request, Guid ownerId, CancellationToken ct = default);
        Task UpdateNicknameAsync(Guid ownerId, Guid targetUserId, string? nickname, CancellationToken ct = default);
        Task BlockAsync(Guid ownerId, Guid targetUserId, CancellationToken ct = default);
        Task UnblockAsync(Guid ownerId, Guid targetUserId, CancellationToken ct = default);
        Task RemoveAsync(Guid ownerId, Guid targetUserId, CancellationToken ct = default);
    }
}
