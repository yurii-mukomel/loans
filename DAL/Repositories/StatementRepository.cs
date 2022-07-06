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
    public class StatementRepository : IRepository<Statement>
    {
        private quick_lendingContext db;

        public StatementRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public async Task CreateAsync(Statement item)
        {
            await db.Statements.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            Statement statement = await db.Statements.FindAsync(id);
            if (statement != null)
                db.Statements.Remove(statement);
        }

        public IQueryable<Statement> Find(Expression<Func<Statement, bool>> predicate)
        {
            return db.Statements.AsNoTracking().Where(predicate);
        }

        public async Task<Statement> GetAsync(int id)
        {
            return await db.Statements.FindAsync(id);
        }

        public async Task<IEnumerable<Statement>> GetAllAsync()
        {
            return await db.Statements.ToListAsync();
        }

        public async Task UpdateAsync(Statement item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public Task<IEnumerable<Statement>> GetPaginatedData(BaseFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
