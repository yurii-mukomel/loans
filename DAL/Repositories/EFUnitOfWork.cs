using DAL.Interfaces;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private quick_lendingContext db;
        private EmployeeRepository employeeRepository;
        private FineRepository fineRepository;
        private PersonRepository personRepository;
        private StatementRepository statementRepository;
        private StatementTypeRepository statementTypeRepository;
        private TransactionRepository transactionRepository;

        public EFUnitOfWork(quick_lendingContext context)
        {
            db = context;
        }

        public IFilter<Person, PersonFilter> PersonFilter
        {
            get
            {
                if (personRepository == null)
                    personRepository = new PersonRepository(db);
                return personRepository;
            }
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository(db);
                return employeeRepository;
            }
        }

        public IRepository<Fine> Fines
        {
            get
            {
                if (fineRepository == null)
                    fineRepository = new FineRepository(db);
                return fineRepository;
            }
        }

        public IRepository<Person> People
        {
            get
            {
                if (personRepository == null)
                    personRepository = new PersonRepository(db);
                return personRepository;
            }
        }

        public IRepository<Statement> Statements
        {
            get
            {
                if (statementRepository == null)
                    statementRepository = new StatementRepository(db);
                return statementRepository;
            }
        }

        public IRepository<StatementType> StatementTypes
        {
            get
            {
                if (statementTypeRepository == null)
                    statementTypeRepository = new StatementTypeRepository(db);
                return statementTypeRepository;
            }
        }

        public IRepository<Transaction> Transactions
        {
            get
            {
                if (transactionRepository == null)
                    transactionRepository = new TransactionRepository(db);
                return transactionRepository;
            }
        }

        public IFilter<Employee, EmployeeFilter> EmployeeFilter
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository(db);
                return employeeRepository;
            }
        }

        private bool disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
