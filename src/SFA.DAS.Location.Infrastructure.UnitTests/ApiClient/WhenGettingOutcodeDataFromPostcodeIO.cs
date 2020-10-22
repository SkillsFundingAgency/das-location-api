using AutoFixture.NUnit3;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Infrastructure.ApiClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Configuration;
using FluentAssertions;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Linq;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient
{
    public class WhenGettingOutcodeDataFromPostcodeIO
    {
        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_And_Outcode_Data_Returned(
            PostcodesLocationApiResponse postcodeResponse,
            string query,
            int count)
        {
            foreach (var postcode in postcodeResponse.Result)
            {
                postcode.Country = "England";
            }
            //Arrange
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodesUrl, query, count)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act
            var actual = await postcodeService.GetAllStartingWithOutcode(query, count);

            //Assert
            actual.Should().BeEquivalentTo(postcodeResponse.Result, options => options.ExcludingMissingMembers());
        }

        [Test, AutoData]
        public async Task Then_Only_Returns_English_Postcodes(
            PostcodesLocationApiResponse postcodeResponse,
            string query,
            int count)
        {
            postcodeResponse.Result[0].Country = "England";
            //Arrange
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodesUrl, query, count)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act
            var actual = await postcodeService.GetAllStartingWithOutcode(query, count);

            //Assert
            actual.Should().BeEquivalentTo(postcodeResponse.Result.Where(c => c.Country == "England"), options => options.ExcludingMissingMembers());
            actual.Count().Should().Be(1);
        }

        [Test, AutoData]
        public async Task Then_If_NotFound_Result_Then_Service_Returns_Null(
        PostcodesLocationApiResponse postcodeResponse,
        string query)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
                StatusCode = HttpStatusCode.NotFound,
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodesUrl, query, 10)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act
            var actual = await postcodeService.GetAllStartingWithOutcode(query);

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
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodesUrl, query, 10)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act Assert

            Assert.ThrowsAsync<HttpRequestException>(() => postcodeService.GetAllStartingWithOutcode(query, 10));
        }

        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_And_Postcode_Data_Returned(
            PostcodesLocationApiResponse postcodeResponse,
            string query)
        {
            foreach (var postcode in postcodeResponse.Result)
            {
                postcode.Country = "England";
            }
            //Arrange
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.DistrictNameUrl, query)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act
            var actual = await postcodeService.GetFullPostcodeDataByOutcode(query);

            //Assert
            actual.Should().BeEquivalentTo(postcodeResponse.Result, options => options.ExcludingMissingMembers());
        }


    }
}


