using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Application.Services.ChannelService
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepo;
        private readonly IChannelMemberRepository _memberRepo;

        public ChannelService(IChannelRepository channelRepo, IChannelMemberRepository memberRepo)
        {
            _channelRepo = channelRepo;
            _memberRepo = memberRepo;
        }

        public async Task<(bool success, string message, ChannelResponse? Data)> CreateChannelAsync(ChannelRequest dto, Guid creatorId)
        {
            var request = new ChannelRequest
            {
                Title = dto.Title,
                Description = dto.Description,
                OwnerId = creatorId
            };

            var entity = await _channelRepo.AddAsync(request);
            return (true, "Channel created.", MapToDto(entity));
        }

        public async Task<(bool success, string message, ChannelResponse? Data)> GetChannelByIdAsync(Guid channelId)
        {
            var entity = await _channelRepo.GetByIdAsync(channelId);
            if (entity is null) return (false, "Channel not found.", null);
            return (true, "OK", MapToDto(entity));
        }

        public async Task<(bool success, string message)> AddSubscriberAsync(Guid channelId, Guid userId)
        {
            var exists = await _channelRepo.ExistsAsync(channelId);
            if (!exists) return (false, "Channel not found.");

            var memberRequest = new ChannelMemberRequest
            {
                ChannelId = channelId,
                UserId = userId,
                IsAdmin = false
            };

            await _memberRepo.AddAsync(memberRequest);
            return (true, "Subscribed.");
        }

        public async Task<(bool success, string message)> RemoveSubscriberAsync(Guid channelId, Guid userId, Guid requesterId)
        {
            var channel = await _channelRepo.GetByIdAsync(channelId);
            if (channel is null) return (false, "Channel not found.");

            // owner can remove anyone; a user can remove themself
            if (requesterId != channel.OwnerId && requesterId != userId)
                return (false, "Not authorized.");

            await _memberRepo.RemoveAsync(channelId, userId);
            return (true, "Unsubscribed.");
        }

        public async Task<(bool success, string message)> SetProfileAsync(Guid channelId, ChannelProfile dto, Guid requesterId)
        {
            var channel = await _channelRepo.GetByIdAsync(channelId);
            if (channel is null) return (false, "Channel not found.");

            if (channel.OwnerId != requesterId)
                return (false, "Not authorized.");

            var profileRequest = new ChannelProfileRequest
            {
                DisplayName = dto.DisplayName,
                Description = dto.Description,
                AvatarUrl = dto.AvatarUrl
            };

            await _channelRepo.SetProfileAsync(channelId, requesterId, profileRequest);
            return (true, "Profile updated.");
        }

        public async Task<(bool success, string message)> DeleteChannelAsync(Guid channelId, Guid requesterId)
        {
            var channel = await _channelRepo.GetByIdAsync(channelId);
            if (channel is null) return (false, "Channel not found.");

            if (channel.OwnerId != requesterId)
                return (false, "Not authorized.");

            await _channelRepo.DeleteAsync(channelId);
            return (true, "Deleted.");
        }

        private static ChannelResponse MapToDto(ChannelResponse entity)
        {
            return new ChannelResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                OwnerId = entity.OwnerId,
                OwnerName = entity.OwnerName,
                CreatedAt = entity.CreatedAt,
                MemberCount = entity.MemberCount
            };
        }
    }
}