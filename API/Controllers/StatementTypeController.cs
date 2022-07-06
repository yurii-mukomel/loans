using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatementTypeController : ControllerBase
    {
        private readonly IStatementTypeService _statementTypeService;

        public StatementTypeController(IStatementTypeService statementTypeService)
        {
            _statementTypeService = statementTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatementTypeDTO>>> GetAll()
        {

            IEnumerable<StatementTypeDTO> statementTypes = await _statementTypeService.GetAll();
            if (statementTypes == null)
                return NotFound();
            return Ok(statementTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatementTypeDTO>> Get([FromRoute] int id)
        {
            StatementTypeDTO statementType = await _statementTypeService.Get(id);
            return Ok(statementType);
        }

        [HttpPost]
        public async Task<ActionResult<StatementTypeDTO>> Post([FromBody] StatementTypeDTO statementType)
        {
            await _statementTypeService.Create(statementType);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<StatementTypeDTO>> Put([FromBody] StatementTypeDTO statementType)
        {

            await _statementTypeService.Update(statementType);

            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StatementTypeDTO>> Delete(int id)
        {

            StatementTypeDTO statementType = await _statementTypeService.Get(id);
            if (statementType == null)
                return NotFound();
            await _statementTypeService.Delete(statementType);
            return Ok();

        }
    }
}
