using secre_chat_api.chat.Domain.DTOS;

namespace secre_chat_api.chat.Application.Services.ChatService
{
    public interface IChatService
    {
        Task<(bool success, string message, ChatResponse? Data)> CreateAsync(ChatRequest dto, Guid creatorId);
        Task<(bool success, string message, ChatResponse? Data)> GetByIdAsync(Guid chatId);
        Task<(bool success, string message, ChatResponse? Data)> UpdateAsync(Guid chatId, ChatRequest dto, Guid requesterId);
        Task<(bool success, string message)> DeleteAsync(Guid chatId, Guid requesterId);
        Task<(bool success, string message)> SetProfileAsync(Guid chatId, GroupProfileRequest dto, Guid requesterId);
    }
}
