using ElevenNote.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace ElevenNote.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
