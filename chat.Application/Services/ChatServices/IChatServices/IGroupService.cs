using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;

namespace secre_chat_api.chat.Application.Services.GroupService
{
    public interface IGroupService
    {
        Task<(bool success, string message, GroupProfileResponse? Data)> CreateGroupAsync(GroupProfileRequest dto, Guid creatorId);
        Task<(bool success, string message, GroupProfileResponse? Data)> GetGroupByIdAsync(Guid groupId);
        Task<(bool success, string message)> AddMemberAsync(Guid groupId, Guid userId, Guid requesterId);
        Task<(bool success, string message)> RemoveMemberAsync(Guid groupId, Guid userId, Guid requesterId);
        Task<(bool success, string message)> SetProfileAsync(Guid groupId, GroupProfile dto, Guid requesterId);
        Task<(bool success, string message)> DeleteGroupAsync(Guid groupId, Guid requesterId);
    }
}
