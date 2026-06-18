using secre_chat_api.chat.Domain.DTOS;

namespace secre_chat_api.chat.Application.Services.MessageService
{
    public interface IMessageService
    {
        Task<(bool success, string message, MessageResponse? Data)> SendMessageAsync(MessageRequest dto, Guid senderId);
        Task<(bool success, string message, MessageResponse? Data)> EditMessageAsync(Guid messageId, string newContent, Guid senderId);
        Task<(bool success, string message)> DeleteMessageAsync(Guid messageId, Guid senderId);
        Task<(bool success, string message, IEnumerable<MessageResponse>? Data)> GetMessagesAsync(Guid chatId);
    }
}
