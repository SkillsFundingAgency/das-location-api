using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SFA.DAS.Location.Api.AppStart;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.Interfaces;


namespace SFA.DAS.Location.Api.UnitTests.AppStart
{
    public class WhenAddingServicesToTheContainer
    {
        [TestCase(typeof(ILocationService))]
        [TestCase(typeof(ILocationImportService))]
        public void Then_The_Dependencies_Are_Correctly_Resolved(Type toResolve)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDatabaseRegistration(new LocationApiConfiguration(), "DEV");
            serviceCollection.AddServiceRegistration();
            
            var provider = serviceCollection.BuildServiceProvider();

            var type = provider.GetService(toResolve);
            Assert.IsNotNull(type);
        }
        
    }
}