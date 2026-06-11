using secre_chat_api.chat.Domain.DTOS.GroupDtos;

namespace secre_chat_api.chat.Application.Services.GroupService
{
    public interface IGroupService
    {
        Task<(bool success, string message, GroupResponseDto? Data)> CreateGroupAsync(GroupRequestDto dto, Guid creatorId);
        Task<(bool success, string message, GroupResponseDto? Data)> GetGroupByIdAsync(Guid groupId);
        Task<(bool success, string message)> AddMemberAsync(Guid groupId, Guid userId, Guid requesterId);
        Task<(bool success, string message)> RemoveMemberAsync(Guid groupId, Guid userId, Guid requesterId);
        Task<(bool success, string message)> SetProfileAsync(Guid groupId, GroupProfileDto dto, Guid requesterId);
        Task<(bool success, string message)> DeleteGroupAsync(Guid groupId, Guid requesterId);
    }
}
