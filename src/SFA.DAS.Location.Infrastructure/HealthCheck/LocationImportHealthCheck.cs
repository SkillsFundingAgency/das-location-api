using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SFA.DAS.Api.Common.Infrastructure;
using SFA.DAS.Location.Domain.Entities;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Infrastructure.HealthCheck
{
    public class LocationImportHealthCheck : IHealthCheck
    {
        private readonly IImportAuditRepository _repository;
        private const string HealthCheckResultDescription = "ONS Location Data Health Check";

        public LocationImportHealthCheck (IImportAuditRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var timer = Stopwatch.StartNew();
            
            var result = await _repository.GetLastImportByType(ImportType.OnsLocation);
            
            timer.Stop();
            var durationString = timer.Elapsed.ToHumanReadableString();

            if (result == null)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, $"{HealthCheckResultDescription}: No location data import ran",null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            if (result.RowsImported == 0)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, $"{HealthCheckResultDescription}: No location data imported",null, new Dictionary<string, object> { { "Duration", durationString } });
            }

            if (DateTime.UtcNow >= result.TimeStarted.AddHours(25))
            {
                return new HealthCheckResult(HealthStatus.Degraded, $"{HealthCheckResultDescription}: Location data is over 25 hours old",null, new Dictionary<string, object> { { "Duration", durationString } });
            }
            
            return HealthCheckResult.Healthy(HealthCheckResultDescription, new Dictionary<string, object>
            {
                {"Duration", durationString }
            });
        }
    }
}