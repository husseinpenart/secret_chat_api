using Microsoft.AspNetCore.Mvc;

namespace secre_chat_api.chat.API.Controllers.AuthController
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
