using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Domain.Entities;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Infrastructure.HealthCheck;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Infrastructure.UnitTests.HealthChecks
{
    public class WhenCheckingLocationImportHealth
    {
        [Test, MoqAutoData]
        public async Task Then_If_There_Is_No_Import_Item_It_Reports_Unhealthy(
            [Frozen] Mock<IImportAuditRepository> auditRepository,
            HealthCheckContext context,
            LocationImportHealthCheck healthCheck)
        {
            //Arrange
            auditRepository.Setup(x => x.GetLastImportByType(ImportType.OnsLocation)).ReturnsAsync((ImportAudit) null);
            
            //Act
            var actual = await healthCheck.CheckHealthAsync(context);
            
            //Assert
            actual.Status.Should().Be(HealthStatus.Unhealthy);
        }
        
        [Test, MoqAutoData]
        public async Task Then_If_There_Is_No_Rows_Imported_It_Reports_Unhealthy(
            [Frozen] Mock<IImportAuditRepository> auditRepository,
            HealthCheckContext context,
            LocationImportHealthCheck healthCheck)
        {
            //Arrange
            auditRepository.Setup(x => x.GetLastImportByType(ImportType.OnsLocation)).ReturnsAsync(new ImportAudit(DateTime.Now,0, ""));
            
            //Act
            var actual = await healthCheck.CheckHealthAsync(context);
            
            //Assert
            actual.Status.Should().Be(HealthStatus.Unhealthy);
        }

        
        [Test, MoqAutoData]
        public async Task Then_If_The_Import_Is_Over_Twenty_Five_Hours_Old_Show_Degraded(
            [Frozen] Mock<IImportAuditRepository> auditRepository,
            HealthCheckContext context,
            LocationImportHealthCheck healthCheck)
        {
            //Arrange
            auditRepository.Setup(x => x.GetLastImportByType(ImportType.OnsLocation)).ReturnsAsync(new ImportAudit(DateTime.UtcNow.AddHours(-25),10,""));
            
            //Act
            var actual = await healthCheck.CheckHealthAsync(context);
            
            //Assert
            actual.Status.Should().Be(HealthStatus.Degraded);
        }

        [Test, MoqAutoData]
        public async Task Then_If_The_Import_Is_Under_Twenty_Five_Hours_Old_Show_Healthy(
            [Frozen] Mock<IImportAuditRepository> auditRepository,
            HealthCheckContext context,
            LocationImportHealthCheck healthCheck)
        {
            //Arrange
            auditRepository.Setup(x => x.GetLastImportByType(ImportType.OnsLocation)).ReturnsAsync(new ImportAudit(DateTime.UtcNow.AddHours(25).AddMinutes(-1),10, ""));
            
            //Act
            var actual = await healthCheck.CheckHealthAsync(context);
            
            //Assert
            actual.Status.Should().Be(HealthStatus.Healthy);
        }
    }
}