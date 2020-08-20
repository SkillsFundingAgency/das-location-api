using FluentAssertions;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SFA.DAS.Location.Api.AppStart;
using SFA.DAS.Location.Data;
using SFA.DAS.Location.Domain.Configuration;

namespace SFA.DAS.Location.Api.UnitTests.AppStart
{
    public class WhenAddingDataContextToTheContainer
    {
        [Test]
        public void Then_The_Context_Is_Resolved()
        {
            var config = new LocationApiConfiguration
            {
                ConnectionString = "test"
            };
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDatabaseRegistration(config,"TEST");

            var provider = serviceCollection.BuildServiceProvider();

            var type = provider.GetService<ILocationDataContext>();
            
            Assert.IsNotNull(type);
            var tokenProvider = provider.GetService<AzureServiceTokenProvider>(); 
            Assert.IsNotNull(tokenProvider);
        }
        
        [Test]
        public void Then_The_Context_Is_Resolved_For_Local()
        {
            var config = new LocationApiConfiguration
            {
                ConnectionString = "test"
            };
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDatabaseRegistration(config,"local");

            var provider = serviceCollection.BuildServiceProvider();

            var type = provider.GetService<ILocationDataContext>();
            
            Assert.IsNotNull(type);
            type.GetProviderName().Should().EndWith("SqlServer");
            
        }

        [Test]
        public void Then_The_Context_Is_Resolved_For_Dev()
        {
            var config = new LocationApiConfiguration
            {
                ConnectionString = "test"
            };
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDatabaseRegistration(config,"Dev");

            var provider = serviceCollection.BuildServiceProvider();

            var type = provider.GetService<ILocationDataContext>();
            
            Assert.IsNotNull(type);
            type.GetProviderName().Should().EndWith("InMemory");
            
        }
    }
}