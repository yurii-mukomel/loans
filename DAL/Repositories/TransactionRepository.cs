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
    public class TransactionRepository : IRepository<Transaction>
    {
        private quick_lendingContext db;

        public TransactionRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public async Task CreateAsync(Transaction item)
        {
            await db.Transactions.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction != null)
                db.Transactions.Remove(transaction);
        }

        public IQueryable<Transaction> Find(Expression<Func<Transaction, bool>> predicate)
        {
            return db.Transactions.AsNoTracking().Where(predicate);
        }

        public async Task<Transaction> GetAsync(int id)
        {
            return await db.Transactions.FindAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await db.Transactions.ToListAsync();
        }

        public async Task UpdateAsync(Transaction item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public Task<IEnumerable<Transaction>> GetPaginatedData(BaseFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
