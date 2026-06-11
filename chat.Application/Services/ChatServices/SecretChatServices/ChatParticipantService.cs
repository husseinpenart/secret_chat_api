using secre_chat_api.chat.Application.Repositories.ChatParticipantRepository;
using secre_chat_api.chat.Domain.DTOS.ChatDtos;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Application.Services.ChatParticipantService
{
    public class ChatParticipantService : IChatParticipantService
    {
        private readonly IChatParticipantRepository _repo;
        public ChatParticipantService(IChatParticipantRepository repo) => _repo = repo;

        public async Task<(bool success, string message)> AddParticipantAsync(Guid chatId, Guid userId, Guid requesterId)
        {
            var result = await _repo.AddAsync(chatId, userId, requesterId);
            return result ? (true, "Participant added.") : (false, "Failed or unauthorized.");
        }

        public async Task<(bool success, string message)> RemoveParticipantAsync(Guid chatId, Guid userId, Guid requesterId)
        {
            var result = await _repo.RemoveAsync(chatId, userId, requesterId);
            return result ? (true, "Participant removed.") : (false, "Failed or unauthorized.");
        }

        public async Task<(bool success, string message)> UpdateAdminStatusAsync(Guid chatId, Guid userId, bool isAdmin, Guid requesterId)
        {
            var result = await _repo.UpdateAdminStatusAsync(chatId, userId, isAdmin, requesterId);
            return result ? (true, "Admin status updated.") : (false, "Failed or unauthorized.");
        }

        public async Task<(bool success, string message, IEnumerable<ChatParticipantResponseDto>? Data)> GetParticipantsAsync(Guid chatId)
        {
            var data = await _repo.GetByChatIdAsync(chatId);
            return (true, "OK", data);
        }
    }
}
