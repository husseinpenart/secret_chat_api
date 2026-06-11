using secre_chat_api.chat.Domain.DTOS.ChatDtos;

namespace secre_chat_api.chat.Application.Services.ChatService
{
    public interface IChatService
    {
        Task<(bool success, string message, ChatResponseDto? Data)> CreateAsync(ChatRequestDto dto, Guid creatorId);
        Task<(bool success, string message, ChatResponseDto? Data)> GetByIdAsync(Guid chatId);
        Task<(bool success, string message, ChatResponseDto? Data)> UpdateAsync(Guid chatId, ChatRequestDto dto, Guid requesterId);
        Task<(bool success, string message)> DeleteAsync(Guid chatId, Guid requesterId);
        Task<(bool success, string message)> SetProfileAsync(Guid chatId, GroupProfileRequestDto dto, Guid requesterId);
    }
}
