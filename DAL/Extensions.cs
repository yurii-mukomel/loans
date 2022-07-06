using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class Extensions
    {
        public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<quick_lendingContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=quick_lending;Username=postgres;Password=AaAmukomel2511+"));

            using (var serviseScope = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviseScope.ServiceProvider.GetService<quick_lendingContext>())
                {
                    context.Database.Migrate();
                }
            }

            services.AddScoped(typeof(IRepository<Employee>), typeof(EmployeeRepository));
            services.AddScoped(typeof(IRepository<Fine>), typeof(FineRepository));
            services.AddScoped(typeof(IRepository<Person>), typeof(PersonRepository));
            services.AddScoped(typeof(IRepository<Statement>), typeof(StatementRepository));
            services.AddScoped(typeof(IRepository<StatementType>), typeof(StatementTypeRepository));
            services.AddScoped(typeof(IRepository<Transaction>), typeof(TransactionRepository));
            services.AddScoped(typeof(IUnitOfWork), typeof(EFUnitOfWork));
        }
    }
}
