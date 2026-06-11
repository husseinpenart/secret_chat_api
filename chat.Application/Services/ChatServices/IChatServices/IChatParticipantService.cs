using secre_chat_api.chat.Domain.DTOS.ChatDtos;

namespace secre_chat_api.chat.Application.Services.ChatParticipantService
{
    public interface IChatParticipantService
    {
        Task<(bool success, string message)> AddParticipantAsync(Guid chatId, Guid userId, Guid requesterId);
        Task<(bool success, string message)> RemoveParticipantAsync(Guid chatId, Guid userId, Guid requesterId);
        Task<(bool success, string message)> UpdateAdminStatusAsync(Guid chatId, Guid userId, bool isAdmin, Guid requesterId);
        Task<(bool success, string message, IEnumerable<ChatParticipantResponseDto>? Data)> GetParticipantsAsync(Guid chatId);
    }
}
