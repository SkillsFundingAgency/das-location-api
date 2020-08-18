using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using SFA.DAS.Location.Api.Infrastructure;

namespace SFA.DAS.Location.Api.AppStart
{
    public static class HealthCheckStartup
    {
        public static void UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponseWriter.WriteJsonResponse
            });
            
            app.UseHealthChecks("/ping", new HealthCheckOptions
            {
                Predicate = (_) => false,
                ResponseWriter = (context, report) => 
                {
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("");
                }
            });
        }
    }
}