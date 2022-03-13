using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SFA.DAS.Location.Api.AppStart;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Api.UnitTests.AppStart
{
    public class WhenAddingRepositoriesToTheContainer
    {
        [TestCase(typeof(IImportAuditRepository))]
        [TestCase(typeof(ILocationRepository))]
        [TestCase(typeof(ILocationImportRepository))]
        public void Then_The_Dependencies_Are_Correctly_Resolved(Type toResolve)
        {
            var config = new LocationApiConfiguration
            {
                ConnectionString = "test"
            };
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDatabaseRegistration(config,"local");
            serviceCollection.AddSingleton(new LocationApiConfiguration());

            var provider = serviceCollection.BuildServiceProvider();

            var type = provider.GetService(toResolve);
            Assert.IsNotNull(type);
        }
    }
}