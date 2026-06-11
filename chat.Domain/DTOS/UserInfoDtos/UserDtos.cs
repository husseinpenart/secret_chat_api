using secre_chat_api.chat.Application.Dictionary;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace secre_chat_api.chat.Domain.DTOS.UserDtos
{

    public class UserProfileRequest
    {
        [Required, MaxLength(100)]
        public string DisplayName { get; set; } = null!;
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
    }

    public class UserProfileResponse
    {
        public Guid ProfileId { get; set; }
        public Guid UserId { get; set; }
        public string DisplayName { get; set; } = null!;
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastSeen { get; set; }
        public bool IsOnline { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string IdentityName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime RegisteredDate { get; set; }
        public UserProfileResponse? ActiveProfile { get; set; }
    }
}
