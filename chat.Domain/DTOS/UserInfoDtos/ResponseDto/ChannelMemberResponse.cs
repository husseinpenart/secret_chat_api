using secre_chat_api.chat.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.DTOS.UserInfoDtos.ResponseDto
{
    public class ChannelMemberResponse
    {

        public Guid ChannelId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(ChannelId))]
        public ChannelDtos Channel { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; } = null!;
    }
}
