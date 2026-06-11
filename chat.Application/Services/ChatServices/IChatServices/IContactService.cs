
namespace secre_chat_api.chat.Application.Services.ContactService
{
    public interface IContactService
    {
        Task<(bool success, string message, ContactResponseDto? Data)> AddContactAsync(ContactRequestDto dto, Guid userId);
        Task<(bool success, string message)> RemoveContactAsync(Guid contactId, Guid userId);
        Task<(bool success, string message)> BlockContactAsync(Guid contactId, Guid userId);
        Task<(bool success, string message, IEnumerable<ContactResponseDto>? Data)> GetContactsAsync(Guid userId);
    }
}
