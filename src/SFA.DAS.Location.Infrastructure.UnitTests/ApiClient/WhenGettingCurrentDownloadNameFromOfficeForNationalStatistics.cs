using System.Net.Http;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Infrastructure.ApiClient;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient
{
    public class WhenGettingCurrentDownloadNameFromOfficeForNationalStatistics
    {
        [Test, AutoData]
        public void Then_The_Current_Download_Name_Is_Returned()
        {
            //Arrange
            var client = new HttpClient(Mock.Of<HttpMessageHandler>());
            var locationService = new NationalStatisticsLocationService(client);

            //Act
            var actual = locationService.GetName();

            //Assert
            actual.Should().Be(string.Format(Constants.NationalOfficeOfStatisticsLocationUrl, 2000, 0));
        }
    }
}