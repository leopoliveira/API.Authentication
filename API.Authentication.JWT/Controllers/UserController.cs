using API.Authentication.Core.Entities;
using API.Authentication.Core.Repositories;
using API.Authentication.JWT.AuthTokenService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Authentication.JWT.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly UserRepository _repository = new();
        private readonly TokenService _tokenService;

        public UserController(IConfiguration config)
        {
            _tokenService = new(config);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] User user)
        {
            return Ok(_repository.GetUserById(user.Id));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserModel user)
        {
            bool authenticated = _repository.Authenticate(user.UserName, user.Password);

            if (authenticated)
            {
                string token = _tokenService.GenerateToken();

                return Ok(new
                {
                    token
                });
            }

            return Unauthorized("Invalid Credentials.");
        }
    }

    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
