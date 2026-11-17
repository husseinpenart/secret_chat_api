using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace secre_chat_api.chat.API.Controllers;
[Authorize]
public abstract class SecureControllerBase:ControllerBase
{
 protected bool TryGetUserId(out Guid userId)=>Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier),out userId);
 protected IActionResult AuthError()=>Unauthorized(new{success=false,message="Authentication is required."});
}
