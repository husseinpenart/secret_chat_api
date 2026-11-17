using Microsoft.AspNetCore.Mvc;
using secre_chat_api.chat.Application.Services.ContactService;
using secre_chat_api.chat.Domain.DTOS;
namespace secre_chat_api.chat.API.Controllers.ContactController;

[Route("api/contacts")]
public sealed class ContactController : SecureControllerBase
{
    readonly IContactService service; public ContactController(IContactService service) => this.service = service;
    [HttpGet] public async Task<IActionResult> Get() { if (!TryGetUserId(out var id)) return AuthError(); var r = await service.GetContactsAsync(id); return Ok(r.Data); }
    [HttpPost] public async Task<IActionResult> Add([FromBody] ContactRequest d) { if (!TryGetUserId(out var id)) return AuthError(); var r = await service.AddContactAsync(d, id); return r.success ? Ok(r.Data) : BadRequest(r.message); }
    [HttpDelete("{target:guid}")] public async Task<IActionResult> Remove(Guid target) { if (!TryGetUserId(out var id)) return AuthError(); var r = await service.RemoveContactAsync(target, id); return r.success ? NoContent() : Forbid(); }
}
