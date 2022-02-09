using System;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.Location.Data;
using SFA.DAS.Location.Data.Repository;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Api.AppStart
{
    public static class AddDatabaseExtension
    {
        public static void AddDatabaseRegistration(this IServiceCollection services, LocationApiConfiguration config, string environmentName)
        {
            if (environmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddDbContext<LocationDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.Location"), ServiceLifetime.Transient);
            }
            else if (environmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddDbContext<LocationDataContext>(options=>options.UseSqlServer(config.ConnectionString),ServiceLifetime.Transient);
            }
            else
            {
                services.AddSingleton(new AzureServiceTokenProvider());
                services.AddDbContext<LocationDataContext>(ServiceLifetime.Transient);    
            }
            
            services.AddTransient<ILocationDataContext, LocationDataContext>(provider => provider.GetService<LocationDataContext>());
            services.AddTransient(provider => new Lazy<LocationDataContext>(provider.GetService<LocationDataContext>()));

            services.AddTransient<IImportAuditRepository, ImportAuditRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<ILocationImportRepository, LocationImportRepository>();
            services.AddTransient<IPostcodeOutcodeRepository, PostcodeOutcodeRepository>();
        }
    }
}