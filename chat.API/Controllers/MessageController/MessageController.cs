using Microsoft.AspNetCore.Mvc;
using secre_chat_api.chat.Application.Services.MessageService;
using secre_chat_api.chat.Application.Services.ChatParticipantService;
using secre_chat_api.chat.Application.Services.ChannelMemberService;
using secre_chat_api.chat.Domain.DTOS;
namespace secre_chat_api.chat.API.Controllers.MessageController;

[Route("api/messages")]
public sealed class MessageController : SecureControllerBase
{
    readonly IMessageService service; readonly IChatParticipantService chats; readonly IChannelMemberService channels;
    public MessageController(IMessageService service, IChatParticipantService chats, IChannelMemberService channels) { this.service = service; this.chats = chats; this.channels = channels; }
    async Task<bool> Allowed(MessageRequest d, Guid u) { if (d.ChatId is Guid c) { var x = await chats.GetParticipantsAsync(c); return x.Data?.Any(p => p.UserId == u) == true; } if (d.ChannelId is Guid ch) { var x = await channels.GetMembersAsync(ch); return x.Data?.Any(p => p.UserId == u) == true; } return false; }
    [HttpPost] public async Task<IActionResult> Send([FromBody] MessageRequest dto) { if (!TryGetUserId(out var id)) return AuthError(); if ((dto.ChatId is null) == (dto.ChannelId is null) || !await Allowed(dto, id)) return Forbid(); if (string.IsNullOrWhiteSpace(dto.Content) && dto.Attachments.Count == 0) return BadRequest("Message content or attachment is required."); var r = await service.SendMessageAsync(dto, id); return r.success ? Ok(r.Data) : BadRequest(r.message); }
    [HttpGet("chat/{chatId:guid}")] public async Task<IActionResult> Chat(Guid chatId) { if (!TryGetUserId(out var id)) return AuthError(); var x = await chats.GetParticipantsAsync(chatId); if (x.Data?.Any(p => p.UserId == id) != true) return Forbid(); var r = await service.GetMessagesAsync(chatId); return Ok(r.Data); }
    [HttpPut("{messageId:guid}")] public async Task<IActionResult> Edit(Guid messageId, [FromBody] string content) { if (!TryGetUserId(out var id) || string.IsNullOrWhiteSpace(content)) return BadRequest(); var r = await service.EditMessageAsync(messageId, content, id); return r.success ? Ok(r.Data) : Forbid(); }
    [HttpDelete("{messageId:guid}")] public async Task<IActionResult> Delete(Guid messageId) { if (!TryGetUserId(out var id)) return AuthError(); var r = await service.DeleteMessageAsync(messageId, id); return r.success ? NoContent() : Forbid(); }
}
