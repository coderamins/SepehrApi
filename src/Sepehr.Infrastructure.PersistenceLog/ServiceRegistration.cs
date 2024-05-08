using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Interfaces;
using Sepehr.Infrastructure.Persistence.Context;
using Sepehr.Infrastructure.Persistence.Repositories;

namespace Sepehr.Infrastructure.PersistenceLog
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLogInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string constr = configuration.GetValue<string>("ConnectionStrName");

            services.AddDbContext<ApplicationLogDbContext>(options =>
            options.UseSqlServer(
               configuration.GetConnectionString(constr),
               b => b.MigrationsAssembly(typeof(ApplicationLogDbContext).Assembly.FullName)));


            #region Repositories
            services.AddTransient<ITableRecordRemovalRepositoryAsync, TableRecordRemovalRepositoryAsync>();
            #endregion


        }
    }
}