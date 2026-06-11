using secre_chat_api.chat.Application.Repositories.ChatRepository;
using secre_chat_api.chat.Domain.DTOS.ChatDtos;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Application.Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repo;
        public ChatService(IChatRepository repo) => _repo = repo;

        public async Task<(bool success, string message, ChatResponseDto? Data)> CreateAsync(ChatRequestDto dto, Guid creatorId)
        {
            var entity = await _repo.AddAsync(dto, creatorId);
            if (entity is null) return (false, "Failed to create chat.", null);
            return (true, "Chat created.", entity);
        }

        public async Task<(bool success, string message, ChatResponseDto? Data)> GetByIdAsync(Guid chatId)
        {
            var entity = await _repo.GetByIdAsync(chatId);
            if (entity is null) return (false, "Chat not found.", null);
            return (true, "OK", entity);
        }

        public async Task<(bool success, string message, ChatResponseDto? Data)> UpdateAsync(Guid chatId, ChatRequestDto dto, Guid requesterId)
        {
            var entity = await _repo.UpdateAsync(chatId, dto, requesterId);
            if (entity is null) return (false, "Update failed or unauthorized.", null);
            return (true, "Chat updated.", entity);
        }

        public async Task<(bool success, string message)> DeleteAsync(Guid chatId, Guid requesterId)
        {
            var result = await _repo.DeleteAsync(chatId, requesterId);
            return result ? (true, "Deleted.") : (false, "Delete failed or unauthorized.");
        }

        public async Task<(bool success, string message)> SetProfileAsync(Guid chatId, GroupProfileRequestDto dto, Guid requesterId)
        {
            var result = await _repo.SetProfileAsync(chatId, dto, requesterId);
            return result ? (true, "Profile updated.") : (false, "Failed or unauthorized.");
        }
    }
}
