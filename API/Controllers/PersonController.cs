using BLL.DTO;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[JWTAuth_Validation]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        //[Authorize]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAll()
        //{
        //    IEnumerable<PersonDTO> people = await _personService.GetAll();
        //    return Ok(people);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            PersonDTO person = await _personService.Get(id);
            return Ok(person);
        }

        [HttpGet(nameof(PersonFilter))]
        public async Task<ActionResult<PersonDTO>> GetMany([FromQuery, FromBody] PersonFilter filter)
        {
            IEnumerable<PersonDTO> person = await _personService.GetMany(filter);
            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDTO>> Post([FromBody] PersonDTO person)
        {
            await _personService.Create(person);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<PersonDTO>> Put([FromBody] PersonDTO person)
        {
            await _personService.Update(person);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult<PersonDTO>> Delete(int id)
        {
            var person = await _personService.Get(id);
            await _personService.Delete(person);
            return Ok();
        }
    }
}
