using secre_chat_api.chat.Application.Repositories.ChannelRepository;
using secre_chat_api.chat.Domain.DTOS.ChannelDtos;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Application.Services.ChannelService
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _repo;
        public ChannelService(IChannelRepository repo) => _repo = repo;

        public async Task<(bool success, string message, ChannelResponseDto? Data)> CreateAsync(ChannelRequestDto dto, Guid ownerId)
        {
            var entity = await _repo.AddAsync(dto, ownerId);
            if (entity is null) return (false, "Failed to create channel.", null);
            return (true, "Channel created.", entity);
        }

        public async Task<(bool success, string message, ChannelResponseDto? Data)> GetByIdAsync(Guid channelId)
        {
            var entity = await _repo.GetByIdAsync(channelId);
            if (entity is null) return (false, "Channel not found.", null);
            return (true, "OK", entity);
        }

        public async Task<(bool success, string message, ChannelResponseDto? Data)> UpdateAsync(Guid channelId, ChannelRequestDto dto, Guid requesterId)
        {
            var entity = await _repo.UpdateAsync(channelId, dto, requesterId);
            if (entity is null) return (false, "Update failed or unauthorized.", null);
            return (true, "Channel updated.", entity);
        }

        public async Task<(bool success, string message)> DeleteAsync(Guid channelId, Guid requesterId)
        {
            var result = await _repo.DeleteAsync(channelId, requesterId);
            return result ? (true, "Deleted.") : (false, "Delete failed or unauthorized.");
        }

        public async Task<(bool success, string message)> SetProfileAsync(Guid channelId, ChannelProfileRequestDto dto, Guid requesterId)
        {
            var result = await _repo.SetProfileAsync(channelId, dto, requesterId);
            return result ? (true, "Profile updated.") : (false, "Failed or unauthorized.");
        }
    }
}
