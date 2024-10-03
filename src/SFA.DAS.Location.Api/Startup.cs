using System;
using System.Collections.Generic;
using System.IO;
using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SFA.DAS.Api.Common.AppStart;
using SFA.DAS.Api.Common.Configuration;
using SFA.DAS.Api.Common.Infrastructure;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.Location.Api.AppStart;
using SFA.DAS.Location.Api.Infrastructure;
using SFA.DAS.Location.Application.LocationImport.Handlers.ImportLocations;
using SFA.DAS.Location.Data;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Infrastructure.HealthCheck;

namespace SFA.DAS.Location.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            var config = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile("appsettings.Development.json", true)
#endif
                .AddEnvironmentVariables();

            if (!configuration["Environment"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {
                config.AddAzureTableStorage(options =>
                    {
                        options.ConfigurationKeys = configuration["ConfigNames"].Split(",");
                        options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
                        options.EnvironmentName = configuration["Environment"];
                        options.PreFixConfigurationKeys = false;
                    }
                );
            }
            
            _configuration = config.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AzureActiveDirectoryConfiguration>(_configuration.GetSection("AzureAd"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<AzureActiveDirectoryConfiguration>>().Value);
            services.Configure<LocationApiConfiguration>(_configuration.GetSection(nameof(LocationApiConfiguration)));
            services.AddSingleton(cfg => cfg.GetService<IOptions<LocationApiConfiguration>>().Value);
            
            if (!ConfigurationIsLocalOrDev())
            {
                var azureAdConfiguration = _configuration
                    .GetSection("AzureAd")
                    .Get<AzureActiveDirectoryConfiguration>();

                var policies = new Dictionary<string, string>
                {
                    {PolicyNames.Default, RoleNames.Default},
                    {PolicyNames.DataLoad, RoleNames.DataLoad}
                };

                services.AddAuthentication(azureAdConfiguration, policies);
            }
            var locationApiConfiguration = _configuration
                .GetSection(nameof(LocationApiConfiguration))
                .Get<LocationApiConfiguration>();
            
            services.AddDatabaseRegistration(locationApiConfiguration, _configuration["Environment"]);
            services.AddServiceRegistration();
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ImportDataCommand).Assembly));
            
            if (_configuration["Environment"] != "DEV")
            {
                services.AddHealthChecks()
                    .AddDbContextCheck<LocationDataContext>()
                    .AddCheck<LocationImportHealthCheck>("ONS Location Data Health Check",
                    HealthStatus.Unhealthy,
                    new[] {"ready"});
            }
            
            services
                .AddMvc(o =>
                {
                    if (!ConfigurationIsLocalOrDev())
                    {
                        o.Conventions.Add(new AuthorizeControllerModelConvention(new List<string>{PolicyNames.DataLoad}));
                    }
                    o.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
                });

            services.AddOpenTelemetryRegistration(_configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]!);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocationApi", Version = "v1" });
                c.SwaggerDoc("operations", new OpenApiInfo { Title = "LocationApi operations" });
                c.OperationFilter<SwaggerVersionHeaderFilter>();
            });
            services.AddApiVersioning(opt => {
                opt.ApiVersionReader = new HeaderApiVersionReader("X-Version");
            });
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocationAPI v1");
                c.SwaggerEndpoint("/swagger/operations/swagger.json", "Operations v1");
                c.RoutePrefix = string.Empty;
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();
            
            if (_configuration["Environment"] != "DEV")
            {
                app.UseHealthChecks();
            }

            app.UseRouting();
            app.UseEndpoints(builder =>
            {
                builder.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Providers}/{action=Index}/{id?}");
            });
        }
        
        private bool ConfigurationIsLocalOrDev()
        {
            return _configuration["Environment"].Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase) ||
                   _configuration["Environment"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}