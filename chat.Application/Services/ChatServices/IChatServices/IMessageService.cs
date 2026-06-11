using secre_chat_api.chat.Domain.DTOS.MessageDtos;

namespace secre_chat_api.chat.Application.Services.MessageService
{
    public interface IMessageService
    {
        Task<(bool success, string message, MessageResponseDto? Data)> SendMessageAsync(MessageRequestDto dto, Guid senderId);
        Task<(bool success, string message, MessageResponseDto? Data)> EditMessageAsync(Guid messageId, string newContent, Guid senderId);
        Task<(bool success, string message)> DeleteMessageAsync(Guid messageId, Guid senderId);
        Task<(bool success, string message, IEnumerable<MessageResponseDto>? Data)> GetMessagesAsync(Guid chatId);
    }
}
