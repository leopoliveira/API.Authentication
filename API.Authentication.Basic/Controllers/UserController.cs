using API.Authentication.Core.Entities;
using API.Authentication.Core.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace API.Authentication.Basic.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly UserRepository _repository = new();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_repository.GetUserById(id)?.Username);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            return Ok(_repository.GetUserById(user.Id));
        }

        [HttpPost]
        public IActionResult Authenticate(string userName, string password)
        {
            return _repository.Authenticate(userName, password) ? Ok() : Unauthorized();
        }
    }
}
