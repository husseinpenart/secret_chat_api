using secre_chat_api.chat.Application.Repositories.ContactRepository;
using secre_chat_api.chat.Domain.DTOS.ContactDtos;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Application.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repo;
        public ContactService(IContactRepository repo) => _repo = repo;

        public async Task<(bool success, string message, ContactResponseDto? Data)> AddContactAsync(ContactRequestDto dto, Guid userId)
        {
            var entity = await _repo.AddAsync(dto, userId);
            if (entity is null) return (false, "Failed to add contact.", null);
            return (true, "Contact added.", entity);
        }

        public async Task<(bool success, string message)> RemoveContactAsync(Guid contactId, Guid userId)
        {
            var result = await _repo.RemoveAsync(contactId, userId);
            return result ? (true, "Contact removed.") : (false, "Failed or unauthorized.");
        }

        public async Task<(bool success, string message)> BlockContactAsync(Guid contactId, Guid userId)
        {
            var result = await _repo.BlockAsync(contactId, userId);
            return result ? (true, "Contact blocked.") : (false, "Failed or unauthorized.");
        }

        public async Task<(bool success, string message, IEnumerable<ContactResponseDto>? Data)> GetContactsAsync(Guid userId)
        {
            var data = await _repo.GetByUserIdAsync(userId);
            return (true, "OK", data);
        }
    }
}
