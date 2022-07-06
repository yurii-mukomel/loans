using BLL.DTO;
using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeService
    {
        //Task<IEnumerable<EmployeeResponseDTO>> GetAll();
        Task<IEnumerable<EmployeeResponseDTO>> GetMany(EmployeeFilter filter);
        Task<EmployeeResponseDTO> Get(int id);
        Task Create(EmployeeRequestDTO employee);
        Task Delete(EmployeeResponseDTO employee);
    }
}
