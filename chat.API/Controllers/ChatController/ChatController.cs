using Microsoft.AspNetCore.Mvc;
using secre_chat_api.chat.Application.Services.ChatService;
using secre_chat_api.chat.Application.Services.ChatParticipantService;
using secre_chat_api.chat.Domain.DTOS;
namespace secre_chat_api.chat.API.Controllers.ChatController;
[Route("api/chats")]
public sealed class ChatController:SecureControllerBase
{
 readonly IChatService service;readonly IChatParticipantService participants;
 public ChatController(IChatService service,IChatParticipantService participants){this.service=service;this.participants=participants;}
 async Task<bool>Member(Guid c,Guid u){var p=await participants.GetParticipantsAsync(c);return p.Data?.Any(x=>x.UserId==u)==true;}
 [HttpPost]public async Task<IActionResult>Create([FromBody]ChatRequest dto){if(!TryGetUserId(out var id))return AuthError();var r=await service.CreateAsync(dto,id);return r.success?Ok(r.Data):BadRequest(r.message);}
 [HttpGet("{chatId:guid}")]public async Task<IActionResult>Get(Guid chatId){if(!TryGetUserId(out var id)||!await Member(chatId,id))return Forbid();var r=await service.GetByIdAsync(chatId);return r.success?Ok(r.Data):NotFound(r.message);}
 [HttpDelete("{chatId:guid}")]public async Task<IActionResult>Delete(Guid chatId){if(!TryGetUserId(out var id)||!await Member(chatId,id))return Forbid();var r=await service.DeleteAsync(chatId,id);return r.success?NoContent():Forbid();}
}
