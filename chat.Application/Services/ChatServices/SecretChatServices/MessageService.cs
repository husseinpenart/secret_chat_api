using secre_chat_api.chat.Application.Repositories.MessageRepository;
using secre_chat_api.chat.Domain.DTOS.MessageDtos;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Application.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repo;
        public MessageService(IMessageRepository repo) => _repo = repo;

        public async Task<(bool success, string message, MessageResponseDto? Data)> SendAsync(MessageRequestDto dto, Guid senderId)
        {
            var entity = await _repo.AddAsync(dto, senderId);
            if (entity is null) return (false, "Failed to send message.", null);
            return (true, "Message sent.", entity);
        }

        public async Task<(bool success, string message, MessageResponseDto? Data)> EditAsync(Guid messageId, string newContent, Guid requesterId)
        {
            var entity = await _repo.EditAsync(messageId, newContent, requesterId);
            if (entity is null) return (false, "Edit failed or unauthorized.", null);
            return (true, "Message edited.", entity);
        }

        public async Task<(bool success, string message)> DeleteAsync(Guid messageId, Guid requesterId)
        {
            var result = await _repo.DeleteAsync(messageId, requesterId);
            return result ? (true, "Deleted.") : (false, "Delete failed or unauthorized.");
        }

        public async Task<(bool success, string message, IEnumerable<MessageResponseDto>? Data)> GetByChatAsync(Guid chatId)
        {
            var data = await _repo.GetByChatIdAsync(chatId);
            return (true, "OK", data);
        }

        public async Task<(bool success, string message, IEnumerable<MessageResponseDto>? Data)> GetByChannelAsync(Guid channelId)
        {
            var data = await _repo.GetByChannelIdAsync(channelId);
            return (true, "OK", data);
        }
    }
}
