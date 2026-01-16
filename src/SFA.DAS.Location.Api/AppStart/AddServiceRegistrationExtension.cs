using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using SFA.DAS.Location.Application.Addresses.Services;
using SFA.DAS.Location.Application.Location.Services;
using SFA.DAS.Location.Application.LocationImport.Services;
using SFA.DAS.Location.Application.Postcode.Services;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Infrastructure.ApiClient;
using System;
using System.Net.Http;

namespace SFA.DAS.Location.Api.AppStart;

public static class AddServiceRegistrationExtension
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {
        services.AddTransient<IAddressesService, AddressesService>();
        services.AddTransient<ILocationImportService, LocationImportService>();
        services.AddTransient<ILocationService, LocationService>();
        services.AddTransient<IPostcodeService, PostcodeService>();
        services.AddTransient<IPostcodeApiV2Service, PostCodeApiV2Service>();
            
        services.AddHttpClient<INationalStatisticsLocationService, NationalStatisticsLocationService>();
            
        services.AddHttpClient<IOsPlacesApiService, OsPlacesApiService>()
            .AddPolicyHandler(HttpClientRetryPolicy());
            
        services.AddHttpClient<IPostcodeApiService, PostcodeApiService>()
            .AddPolicyHandler(HttpClientRetryPolicy());
    }
    private static IAsyncPolicy<HttpResponseMessage> HttpClientRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.BadRequest)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                retryAttempt)));
    }
}