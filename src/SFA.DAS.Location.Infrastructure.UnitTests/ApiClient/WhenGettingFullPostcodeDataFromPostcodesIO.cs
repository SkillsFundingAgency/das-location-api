using AutoFixture.NUnit3;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Infrastructure.ApiClient;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Configuration;
using FluentAssertions;
using System.Net;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient
{
    public class WhenGettingFullPostcodeDataFromPostcodesIO
    {
        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_And_Postcode_Data_Returned(
        PostcodeLocationApiResponse postcodeResponse,
        string query)
        {
            postcodeResponse.Result.Country = "England";
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
                StatusCode = HttpStatusCode.Accepted,
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodeUrl, query)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act
            var actual = await postcodeService.GetPostcodeData(query);

            //Assert
            actual.Should().BeEquivalentTo(postcodeResponse.Result, options => options.ExcludingMissingMembers());
        }

        [Test, AutoData]
        public async Task Then_If_Postcode_Is_Not_English_Returns_Null(
        PostcodeLocationApiResponse postcodeResponse,
        string query)
        {
            postcodeResponse.Result.Country = "Scotland";
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
                StatusCode = HttpStatusCode.Accepted,
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodeUrl, query)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act
            var actual = await postcodeService.GetPostcodeData(query);

            //Assert
            actual.Should().BeNull();
        }

        [Test, AutoData]
        public async Task Then_If_NotFound_Result_Then_Service_Returns_Null(
        PostcodeLocationApiResponse postcodeResponse,
        string query)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
                StatusCode = HttpStatusCode.NotFound,
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodeUrl, query)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act
            var actual = await postcodeService.GetPostcodeData(query);

            //Assert            
            actual.Should().Be(null);
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
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodeUrl, query)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act Assert
            Assert.ThrowsAsync<HttpRequestException>(() => postcodeService.GetPostcodeData(query));
        }
    }
}
