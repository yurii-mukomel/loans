using BLL.DTO;
using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPersonService
    {
        //Task<IEnumerable<PersonDTO>> GetAll();
        Task<IEnumerable<PersonDTO>> GetMany(PersonFilter filter);
        Task<PersonDTO> Get(int id);
        Task Create(PersonDTO person);
        Task Update(PersonDTO person);
        Task Delete(PersonDTO person);
    }
}
