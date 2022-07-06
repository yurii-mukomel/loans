using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class FineRepository : IRepository<Fine>
    {
        private quick_lendingContext db;

        public FineRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public async Task CreateAsync(Fine item)
        {
            await db.Fines.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            Fine fine = await db.Fines.FindAsync();
            if (fine != null)
                db.Fines.Remove(fine);
        }

        public IQueryable<Fine> Find(Expression<Func<Fine, bool>> predicate)
        {
            return db.Fines.AsNoTracking().Where(predicate);
        }

        public async Task<Fine> GetAsync(int id)
        {
            return await db.Fines.FindAsync(id);
        }

        public async Task<IEnumerable<Fine>> GetAllAsync()
        {
            return await db.Fines.ToListAsync();
        }

        public async Task UpdateAsync(Fine item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public Task<IEnumerable<Fine>> GetPaginatedData(BaseFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
