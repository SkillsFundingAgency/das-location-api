using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Infrastructure.ApiClient;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient
{
    public class WhenGettingDataFromOfficeForNationalStatistics
    {
        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_And_Location_Data_Returned(
            OnsLocationApiResponse importLocations)
        {
            //Arrange
            importLocations.ExceededTransferLimit = false;
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(importLocations)),
                StatusCode = HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.NationalOfficeOfStatisticsLocationUrl,2000,0)));
            var client = new HttpClient(httpMessageHandler.Object);
            var locationService = new LocationService(client);
            
            //Act
            var actual = await locationService.GetLocations();
            
            //Assert
            actual.Should().BeEquivalentTo(importLocations.Features);
        }
        
        [Test]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown()
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.BadRequest
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.NationalOfficeOfStatisticsLocationUrl,2000,0)));
            var client = new HttpClient(httpMessageHandler.Object);
            var locationService = new LocationService(client);
            
            //Act Assert
            Assert.ThrowsAsync<HttpRequestException>(() => locationService.GetLocations());
            
        }
    }
}