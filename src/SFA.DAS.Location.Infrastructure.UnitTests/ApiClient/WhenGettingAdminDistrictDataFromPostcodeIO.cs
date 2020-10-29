using AutoFixture.NUnit3;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Infrastructure.ApiClient;
using System;
using SFA.DAS.Location.Domain.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient
{
    public class WhenGettingAdminDistrictDataFromPostcodeIO
    {
        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_And_District_Data_Returned_For_English_Postcodes(
            PostcodeLocationDistrictApiResponse postcodeResponse,
            string query)
        {
            //Arrange
            postcodeResponse.Result.Country = postcodeResponse.Result.Country.Select(c => {c = "England"; return c;}).ToArray();
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
                StatusCode = HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.DistrictNameUrl, query)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act
            var actual = await postcodeService.GetDistrictData(query);

            //Assert
            actual.Should().BeEquivalentTo(postcodeResponse.Result, options => options
                .Excluding(c=>c.Country)
                .Excluding(c=>c.AdminDistrict)
            );
            actual.Country.Should().Be(postcodeResponse.Result.Country.First());
            actual.AdminDistrict.Should().Be(postcodeResponse.Result.AdminDistrict.First());
        }

        [Test, AutoData]
        public async Task Then_If_NotFound_Result_Then_Service_Returns_Null(
            PostcodeLocationDistrictApiResponse postcodeResponse,
        string query)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
                StatusCode = HttpStatusCode.NotFound,
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.DistrictNameUrl, query)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act
            var actual = await postcodeService.GetDistrictData(query);

            //Assert            
            actual.Should().BeNull();
        }

        [Test, AutoData]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown(string query)
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.BadRequest
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.DistrictNameUrl, query)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act Assert

            Assert.ThrowsAsync<HttpRequestException>(() => postcodeService.GetDistrictData(query));
        }
    }
}
