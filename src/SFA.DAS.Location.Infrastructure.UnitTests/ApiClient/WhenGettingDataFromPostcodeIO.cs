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

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient
{
    public class WhenGettingDataFromPostcodeIO
    {
        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_And_Outcode_Data_Returned(
            PostcodesLocationApiResponse postcodeResponse,
            string query,
            int count)
        {
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
            actual.Should().BeEquivalentTo(postcodeResponse.Result);
        }

        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_And_Postcode_Data_Returned(
            PostcodeLocationApiResponse postcodeResponse,
            string query)
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodeUrl, query)));
            var client = new HttpClient(httpMessageHandler.Object);
            var postcodeService = new PostcodeApiService(client);

            //Act
            var actual = await postcodeService.GetPostcodeData(query);

            //Assert
            actual.Should().BeEquivalentTo(postcodeResponse.Result);
        }
    }
}
