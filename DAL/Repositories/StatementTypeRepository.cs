using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StatementTypeRepository : IRepository<StatementType>
    {
        private quick_lendingContext db;

        public StatementTypeRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public async Task CreateAsync(StatementType item)
        {
            await db.StatementTypes.AddAsync(item);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            StatementType type = await db.StatementTypes.FindAsync(id);
            if (type != null)
            {
                db.StatementTypes.Remove(type);
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<StatementType> Find(Expression<Func<StatementType, bool>> predicate)
        {
            return db.StatementTypes.AsNoTracking().Where(predicate);
        }

        public async Task<StatementType> GetAsync(int id)
        {
            return await db.StatementTypes.AsNoTracking().FirstOrDefaultAsync(st => st.Id == id);
        }

        public async Task<IEnumerable<StatementType>> GetAllAsync()
        {
            return await db.StatementTypes.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(StatementType item)
        {
            db.StatementTypes.Update(item);
            await db.SaveChangesAsync();
        }
    }
}
