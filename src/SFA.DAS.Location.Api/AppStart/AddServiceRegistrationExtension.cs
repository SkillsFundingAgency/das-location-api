using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.Location.Application.LocationImport.Services;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Infrastructure.ApiClient;

namespace SFA.DAS.Location.Api.AppStart
{
    public static class AddServiceRegistrationExtension
    {
        public static void AddServiceRegistration(this IServiceCollection services)
        {
            services.AddHttpClient<INationalStatisticsLocationService, NationalStatisticsLocationService>();
            services.AddTransient<ILocationImportService, LocationImportService>();
        }
    }
}