using Microsoft.AspNetCore.Mvc;
using secre_chat_api.chat.Application.Services.ChannelService;
using secre_chat_api.chat.Application.Services.ChannelMemberService;
using secre_chat_api.chat.Domain.DTOS;
namespace secre_chat_api.chat.API.Controllers.ChannelController;

[Route("api/channels")]
public sealed class ChannelController : SecureControllerBase
{
    readonly IChannelService service; readonly IChannelMemberService members;
    public ChannelController(IChannelService service, IChannelMemberService members) { this.service = service; this.members = members; }
    [HttpPost] public async Task<IActionResult> Create([FromBody] ChannelRequest d) { if (!TryGetUserId(out var id)) return AuthError(); var r = await service.CreateChannelAsync(d, id); return r.success ? Ok(r.Data) : BadRequest(r.message); }
    [HttpGet("{id:guid}")] public async Task<IActionResult> Get(Guid id) { if (!TryGetUserId(out var u)) return AuthError(); var m = await members.GetMembersAsync(id); if (m.Data?.Any(x => x.UserId == u) != true) return Forbid(); var r = await service.GetChannelByIdAsync(id); return r.success ? Ok(r.Data) : NotFound(r.message); }
    [HttpPost("{id:guid}/subscribe")] public async Task<IActionResult> Subscribe(Guid id) { if (!TryGetUserId(out var u)) return AuthError(); var r = await service.AddSubscriberAsync(id, u); return r.success ? Ok() : BadRequest(r.message); }
    [HttpDelete("{id:guid}/subscribe")] public async Task<IActionResult> Unsubscribe(Guid id) { if (!TryGetUserId(out var u)) return AuthError(); var r = await service.RemoveSubscriberAsync(id, u, u); return r.success ? NoContent() : Forbid(); }
}
