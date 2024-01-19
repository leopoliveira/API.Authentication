using API.Authentication.Core.Entities;
using API.Authentication.Core.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Authentication.Bearer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly PersonRepository _repository = new();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetPersons());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_repository.GetPersonById(id));
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(_repository.GetPersonByName(name));
        }

        [HttpGet("{cpf}")]
        public IActionResult GetByCPF(string cpf)
        {
            return Ok(_repository.GetPersonByCPF(cpf));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            return Ok(_repository.GetPersonById(person.Id));
        }
    }
}
