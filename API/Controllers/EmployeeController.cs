using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;


        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponseDTO>> Get(int id)
        {
            var employee = await _employeeService.Get(id);
            return Ok(employee);
        }

        //[HttpGet(nameof(EmployeeFilter))]
        //public async Task<ActionResult<EmployeeResponseDTO>> GetMany(EmployeeFilter filter)
        //{
        //    IEnumerable<EmployeeResponseDTO> employee = null;
        //    return Ok(employee);
        //}

        [HttpPost]
        public async Task<ActionResult<EmployeeRequestDTO>> Post([FromForm] EmployeeRequestDTO employee)
        {
            await _employeeService.Create(employee);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeResponseDTO>> Delete(int id)
        {
            EmployeeResponseDTO employee = await _employeeService.Get(id);
            if (employee == null)
                return NoContent();
            await _employeeService.Delete(employee);
            return Ok();
        }
    }
}
