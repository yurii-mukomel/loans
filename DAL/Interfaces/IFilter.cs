using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IFilter<T, U> where T : class where U : class
    {
        Task<IEnumerable<T>> GetPaginatedData(U filter);
    }
}
