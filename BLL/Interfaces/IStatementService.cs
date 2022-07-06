using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStatementService
    {
        Task<IEnumerable<StatementDTO>> GetAll();
        Task<StatementDTO> Get(int id);
        Task Create(StatementDTO statementDTO);
        Task Update(StatementDTO statementDTO);
        Task Delete(StatementDTO statementDTO);
    }
}
