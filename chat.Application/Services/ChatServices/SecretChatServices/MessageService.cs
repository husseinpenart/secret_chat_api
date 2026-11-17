using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Application.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repo;
        private readonly IMessageCryptoService _crypto;
        public MessageService(IMessageRepository repo, IMessageCryptoService crypto) { _repo = repo; _crypto = crypto; }

        public async Task<(bool success, string message, MessageResponse? Data)> SendMessageAsync(MessageRequest dto, Guid senderId)
        {
            if (dto.Content is not null) dto.Content = _crypto.Protect(dto.Content);
            var entity = await _repo.AddAsync(dto, senderId);
            if (entity?.Content is not null) entity.Content = _crypto.Unprotect(entity.Content);
            if (entity is null) return (false, "Failed to send message.", null);
            return (true, "Message sent.", entity);
        }

        public async Task<(bool success, string message, MessageResponse? Data)> EditMessageAsync(Guid messageId, string newContent, Guid requesterId)
        {
            var entity = await _repo.EditAsync(messageId, _crypto.Protect(newContent), requesterId);
            if (entity?.Content is not null) entity.Content = _crypto.Unprotect(entity.Content);
            if (entity is null) return (false, "Edit failed or unauthorized.", null);
            return (true, "Message edited.", entity);
        }

        public async Task<(bool success, string message)> DeleteMessageAsync(Guid messageId, Guid requesterId)
        {
            var result = await _repo.SoftDeleteAsync(messageId, requesterId);
            return result ? (true, "Deleted.") : (false, "Delete failed or unauthorized.");
        }

        public async Task<(bool success, string message, IEnumerable<MessageResponse>? Data)> GetMessagesAsync(Guid chatId)
        {
            var data = await _repo.GetByChatIdAsync(chatId);
            foreach (var message in data) if (message.Content is not null) message.Content = _crypto.Unprotect(message.Content);
            return (true, "OK", data);
        }

        public async Task<(bool success, string message, IEnumerable<MessageResponse>? Data)> GetByChannelAsync(Guid channelId)
        {
            var data = await _repo.GetByChannelIdAsync(channelId);
            return (true, "OK", data);
        }
    }
}
