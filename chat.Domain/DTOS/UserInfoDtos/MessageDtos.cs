using secre_chat_api.chat.Domain.Entities;

namespace secre_chat_api.chat.Domain.DTOS;

public class MessageRequest
{
    // SenderId comes from auth token, not request body
    public Guid? ChatId { get; set; }
    public Guid? ChannelId { get; set; }
    public Guid? ReplyToMessageId { get; set; }
    public MessageType Type { get; set; } = MessageType.Text;
    // nullable — media-only messages may have no text
    public string? Content { get; set; }
    public List<MediaAttachmentRequest> Attachments { get; set; } = new();
}

public class MessageResponse
{
    public Guid MessageId { get; set; }
    public Guid SenderId { get; set; }
    public string SenderName { get; set; } = null!;
    public string? SenderAvatarUrl { get; set; }
    public Guid? ChatId { get; set; }
    public Guid? ChannelId { get; set; }
    public Guid? ReplyToMessageId { get; set; }
    public MessageType Type { get; set; }
    public string? Content { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime SentAt { get; set; }
    public DateTime? EditedAt { get; set; }
    public List<MediaAttachmentResponse> Attachments { get; set; } = new();
}

public class MediaAttachmentRequest
{
    public MediaType MediaType { get; set; }
    public string Url { get; set; } = null!;
    public string? FileName { get; set; }
    public long? FileSizeBytes { get; set; }
    public string? MimeType { get; set; }
}

public class MediaAttachmentResponse
{
    public Guid AttachmentId { get; set; }
    public MediaType MediaType { get; set; }
    public string Url { get; set; } = null!;
    public string? FileName { get; set; }
    public long? FileSizeBytes { get; set; }
    public string? MimeType { get; set; }
}
