using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStatementTypeService
    {
        Task<IEnumerable<StatementTypeDTO>> GetAll();
        Task<StatementTypeDTO> Get(int id);
        Task Update(StatementTypeDTO statementTypeDTO);
        Task Create(StatementTypeDTO statementTypeDTO);
        Task Delete(StatementTypeDTO statementTypeDTO);
    }
}
