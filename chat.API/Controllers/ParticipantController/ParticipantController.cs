using Microsoft.AspNetCore.Mvc;
using secre_chat_api.chat.Application.Services.ChatParticipantService;
namespace secre_chat_api.chat.API.Controllers.ParticipantController;
[Route("api/chats/{chatId:guid}/participants")]
public sealed class ParticipantController:SecureControllerBase
{
 readonly IChatParticipantService service;public ParticipantController(IChatParticipantService service)=>this.service=service;
 [HttpGet]public async Task<IActionResult>Get(Guid chatId){if(!TryGetUserId(out var id))return AuthError();var x=await service.GetParticipantsAsync(chatId);if(x.Data?.Any(p=>p.UserId==id)!=true)return Forbid();return Ok(x.Data);}
 [HttpPost("{userId:guid}")]public async Task<IActionResult>Add(Guid chatId,Guid userId){if(!TryGetUserId(out var id))return AuthError();var r=await service.AddParticipantAsync(chatId,userId,id);return r.success?Ok():Forbid();}
 [HttpDelete("{userId:guid}")]public async Task<IActionResult>Remove(Guid chatId,Guid userId){if(!TryGetUserId(out var id))return AuthError();var r=await service.RemoveParticipantAsync(chatId,userId,id);return r.success?NoContent():Forbid();}
}
