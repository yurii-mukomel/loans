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
    public class EmployeeRepository : IRepository<Employee>, IFilter<Employee, EmployeeFilter>
    {
        private quick_lendingContext db;

        public EmployeeRepository(quick_lendingContext context)
        {
            this.db = context;
        }

        public async Task CreateAsync(Employee item)
        {
            await db.Employees.AddAsync(item);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<Employee> Find(Expression<Func<Employee, bool>> predicate)
        {
            return db.Employees.AsNoTracking().Where(predicate);
        }

        public async Task<Employee> GetAsync(int id)
        {
            var employee = await db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
                return employee;
            var person = await db.People.AsNoTracking().FirstOrDefaultAsync(p => p.Id == employee.PeopleId);
            return new Employee
            {
                Id = employee.Id,
                PeopleId = employee.PeopleId,
                People = new Person { FirstName = person.FirstName, LastName = person.LastName }
            };
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            IQueryable<Employee> query = db.Employees.AsQueryable().Join(db.People, c => c.PeopleId, p => p.Id,
                (p, c) => new Employee
                {
                    Id = p.Id,
                    PeopleId = p.PeopleId,
                    People = new Person { FirstName = c.FirstName, LastName = c.LastName }
                });
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Employee item)
        {
            db.Employees.Update(item);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetPaginatedData(EmployeeFilter filter)
        {
            IQueryable<Employee> query = db.Employees.AsQueryable()
                .Join(db.People, c => c.PeopleId, p => p.Id,
                (p, c) => new Employee
                {
                    Id = p.Id,
                    PeopleId = p.PeopleId,
                    People = new Person { FirstName = c.FirstName, LastName = c.LastName }
                })
                .Where(p => string.IsNullOrEmpty(filter.FirstName) || p.People.FirstName.Contains(filter.FirstName))
                .Where(p => string.IsNullOrEmpty(filter.LastName) || p.People.LastName.Contains(filter.LastName));

            switch (filter.SortingDirection)
            {
                case true:
                    switch (filter.SortingData)
                    {
                        case EmployeeFilter.Sort.firsName:
                            query = query.OrderBy(p => p.People.FirstName)
                                .ThenBy(p => p.People.LastName);
                            break;
                        case EmployeeFilter.Sort.lastName:
                            query = query.OrderBy(p => p.People.LastName).ThenBy(p => p.People.FirstName);
                            break;
                        default:
                            query = query.OrderBy(p => p.Id);
                            break;
                    }
                    break;
                case false:
                    switch (filter.SortingData)
                    {
                        case EmployeeFilter.Sort.firsName:
                            query = query.OrderByDescending(p => p.People.FirstName)
                                .ThenBy(p => p.People.LastName);
                            break;
                        case EmployeeFilter.Sort.lastName:
                            query = query.OrderByDescending(p => p.People.LastName).ThenBy(p => p.People.FirstName);
                            break;
                        default:
                            query = query.OrderByDescending(p => p.Id);
                            break;
                    }
                    break;
            }

            int totalPersonCount = await query.CountAsync();
            DataProviderHelper<Employee> paginatedResult =
                new DataProviderHelper<Employee>(filter.CurrentPage, filter.ItemsOnPage, totalPersonCount);

            var paginated = paginatedResult.GetPaginatedData(await query.ToListAsync());

            return paginated.Data;
        }
    }
}
