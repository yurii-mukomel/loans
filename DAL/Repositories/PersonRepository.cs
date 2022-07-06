using DAL.Helpers;
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
    public class PersonRepository : IRepository<Person>, IFilter<Person, PersonFilter>
    {
        private quick_lendingContext db;

        public PersonRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public async Task CreateAsync(Person item)
        {
            await db.People.AddAsync(item);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Person person = await db.People.FirstOrDefaultAsync(p => p.Id == id);
            if (person != null)
            {
                db.People.Remove(person);
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<Person> Find(Expression<Func<Person, bool>> predicate)
        {
            return db.People.AsNoTracking().Where(predicate);
        }

        public async Task<Person> GetAsync(int id)
        {
            return await db.People.AsNoTracking().FirstOrDefaultAsync(person => person.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await db.People.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(Person item)
        {
            db.People.Update(item);//.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetPaginatedData(PersonFilter filter)
        {
            IQueryable<Person> query = db.People.AsQueryable()
                .Include(person => person.Statements)
                .Where(p => string.IsNullOrEmpty(filter.FirstName) || p.FirstName.Contains(filter.FirstName))
                .Where(p => string.IsNullOrEmpty(filter.LastName) || p.LastName.Contains(filter.LastName))
                .Where(p => !filter.AgeMoreThan.HasValue || p.Age >= filter.AgeMoreThan)
                .Where(p => !filter.AgeLessThan.HasValue || p.Age <= filter.AgeLessThan);

            switch (filter.SortingDirection)
            {
                case true:
                    switch (filter.SortingData)
                    {
                        case PersonFilter.Sort.firsName:
                            query = query.OrderBy(p => p.FirstName)
                                .ThenBy(p => p.LastName);
                            break;
                        case PersonFilter.Sort.lastName:
                            query = query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
                            break;
                        case PersonFilter.Sort.age:
                            query = query.OrderBy(p => p.Age);
                            break;
                        default:
                            query = query.OrderBy(p => p.Id);
                            break;
                    }
                    break;
                case false:
                    switch (filter.SortingData)
                    {
                        case PersonFilter.Sort.firsName:
                            query = query.OrderByDescending(p => p.FirstName)
                                .ThenBy(p => p.LastName);
                            break;
                        case PersonFilter.Sort.lastName:
                            query = query.OrderByDescending(p => p.LastName).ThenBy(p => p.FirstName);
                            break;
                        case PersonFilter.Sort.age:
                            query = query.OrderByDescending(p => p.Age);
                            break;
                        default:
                            query = query.OrderByDescending(p => p.Id);
                            break;
                    }
                    break;
            }

            int totalPersonCount = await query.CountAsync();
            DataProviderHelper<Person> paginatedResult =
                new DataProviderHelper<Person>(filter.CurrentPage, filter.ItemsOnPage, totalPersonCount);

            var paginated = paginatedResult.GetPaginatedData(await query.ToListAsync());

            return paginated.Data;
        }
    }
}
