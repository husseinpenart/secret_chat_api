using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Application.Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repo;
        public ChatService(IChatRepository repo) => _repo = repo;

        public async Task<(bool success, string message, ChatResponse? Data)> CreateAsync(ChatRequest dto, Guid creatorId)
        {
            var entity = await _repo.AddAsync(dto, creatorId);
            if (entity is null) return (false, "Failed to create chat.", null);
            return (true, "Chat created.", entity);
        }

        public async Task<(bool success, string message, ChatResponse? Data)> GetByIdAsync(Guid chatId)
        {
            var entity = await _repo.GetByIdAsync(chatId);
            if (entity is null) return (false, "Chat not found.", null);
            return (true, "OK", entity);
        }

        public async Task<(bool success, string message, ChatResponse? Data)> UpdateAsync(Guid chatId, ChatRequest dto, Guid requesterId)
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

        public async Task<(bool success, string message)> SetProfileAsync(Guid chatId, GroupProfileRequest dto, Guid requesterId)
        {
            var result = await _repo.SetProfileAsync(chatId, dto, requesterId);
            return result ? (true, "Profile updated.") : (false, "Failed or unauthorized.");
        }
    }
}
