using secre_chat_api.chat.Application.Repositories.ChannelMemberRepository;
using secre_chat_api.chat.Domain.DTOS.ChannelDtos;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Application.Services.ChannelMemberService
{
    public class ChannelMemberService : IChannelMemberService
    {
        private readonly IChannelMemberRepository _repo;
        public ChannelMemberService(IChannelMemberRepository repo) => _repo = repo;

        public async Task<(bool success, string message)> AddMemberAsync(Guid channelId, Guid userId, Guid requesterId)
        {
            var result = await _repo.AddAsync(channelId, userId, requesterId);
            return result ? (true, "Member added.") : (false, "Failed or unauthorized.");
        }

        public async Task<(bool success, string message)> RemoveMemberAsync(Guid channelId, Guid userId, Guid requesterId)
        {
            var result = await _repo.RemoveAsync(channelId, userId, requesterId);
            return result ? (true, "Member removed.") : (false, "Failed or unauthorized.");
        }

        public async Task<(bool success, string message)> UpdateAdminStatusAsync(Guid channelId, Guid userId, bool isAdmin, Guid requesterId)
        {
            var result = await _repo.UpdateAdminStatusAsync(channelId, userId, isAdmin, requesterId);
            return result ? (true, "Admin status updated.") : (false, "Failed or unauthorized.");
        }

        public async Task<(bool success, string message, IEnumerable<ChannelMemberResponseDto>? Data)> GetMembersAsync(Guid channelId)
        {
            var data = await _repo.GetByChannelIdAsync(channelId);
            return (true, "OK", data);
        }
    }
}
