using secre_chat_api.chat.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace secre_chat_api.chat.Domain.DTOS.UserInfoDtos.ResponseDto
{
    public class ChannelResponse
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(OwnerId))]
        public UserEntity Owner { get; set; } = null!;
        public ICollection<ChannelMemberDtos> Members { get; set; } = new List<ChannelMemberDtos>();
        public ICollection<MessageDtos> Messages { get; set; } = new List<MessageDtos>();
    }
}
