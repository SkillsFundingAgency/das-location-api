﻿using AutoFixture.NUnit3;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Location.Infrastructure.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient
{
    public class WhenGettingFindDataFromOsPlaces
    {
        [TestCaseSource(nameof(TestCases))]
        public async Task Then_The_Endpoint_Is_Called_And_Find_Data_Returned(
            OsPlacesApiResponse osPlacesApiResponse, string query, double minMatch, int matchPrecision)
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(osPlacesApiResponse)),
                StatusCode = System.Net.HttpStatusCode.Accepted
            };

            var minMatchBase = Math.Round(minMatch, 1, MidpointRounding.ToZero);
            var config = new LocationApiConfiguration { OsPlacesApiKey = Guid.NewGuid().ToString()};

            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, 
                new Uri(string.Format(Constants.OsPlacesFindUrl, config.OsPlacesApiKey, query, "lpi", minMatchBase, matchPrecision)));
            var client = new HttpClient(httpMessageHandler.Object);
            
            var osPlacesApiService = new OsPlacesApiService(client, config);

            // Act
            var actual = await osPlacesApiService.FindFromLpiDataset(query, minMatch);

            // Assert
            actual.Should().BeEquivalentTo(osPlacesApiResponse.Results.Select(p => (SuggestedAddress)p.Lpi));
        }

        static object[] TestCases =
        {
            new object[] {
                new OsPlacesApiResponse
                {
                    Results = new List<ResultPlacesApiItem>
                    {
                        new ResultPlacesApiItem
                        {
                            Lpi = new LpiResultPlacesApiItem
                            {
                                Uprn = "12345",
                                PaoText = "The Dome",
                                PaoStartNumber = "1",
                                StreetDescription = "Fuller Street",
                                TownName = "Buckminster",
                                PostCodeLocator = "BM1 7YI",
                                PostalAddressCode = "D",
                                Lat = 0.23737578,
                                Lng = 3.18373737,
                                Match = 4.3879
                            }
                        }
                    }
                }, "BM1 7YI", 0.434, 3},
            new object[] {
                new OsPlacesApiResponse
                {
                    Results = new List<ResultPlacesApiItem>
                    {
                        new ResultPlacesApiItem
                        {
                            Lpi = new LpiResultPlacesApiItem
                            {
                                Uprn = "12345",
                                PaoText = null,
                                PaoStartNumber = "1",
                                StreetDescription = "Fuller Street",
                                TownName = "Buckminster",
                                PostCodeLocator = "BM1 7YI",
                                PostalAddressCode = "D",
                                Lat = 0.23737578,
                                Lng = 3.18373737,
                                Match = 4.3879
                            }
                        }
                    }
                }, "BM1 7YI", 0.71, 2}
        };

        [Test]
        public async Task Then_If_NotFound_Result_Then_Service_Returns_Null()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            };

            // Arrange
            var query = "AB1 1AB";
            var config = new LocationApiConfiguration { OsPlacesApiKey = Guid.NewGuid().ToString() };

            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response,
                new Uri(string.Format(Constants.OsPlacesFindUrl, config.OsPlacesApiKey, query, "lpi", 0.4, 1)));
            var client = new HttpClient(httpMessageHandler.Object);

            var osPlacesApiService = new OsPlacesApiService(client, config);

            // Act
            var actual = await osPlacesApiService.FindFromLpiDataset(query, 0.4);

            // Assert
            actual.Should().BeEmpty();
        }

        [Test]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
            };

            // Arrange
            var query = "AB1 1AB";
            var config = new LocationApiConfiguration { OsPlacesApiKey = Guid.NewGuid().ToString() };

            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response,
                new Uri(string.Format(Constants.OsPlacesFindUrl, config.OsPlacesApiKey, query, "lpi", 0.4, 1)));
            var client = new HttpClient(httpMessageHandler.Object);

            var osPlacesApiService = new OsPlacesApiService(client, config);

            // Act & Assert
            Assert.ThrowsAsync<HttpRequestException>(() => osPlacesApiService.FindFromLpiDataset(query, 0.4));
        }

        [Test]
        public void Then_If_Arguments_Are_Out_Of_Range_An_Expception_Is_Thrown()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
            };

            // Arrange
            var query = "AB1 1AB";
            var config = new LocationApiConfiguration { OsPlacesApiKey = Guid.NewGuid().ToString() };

            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response,
                new Uri(string.Format(Constants.OsPlacesFindUrl, config.OsPlacesApiKey, query, "lpi", 1.1, 1)));
            var client = new HttpClient(httpMessageHandler.Object);

            var osPlacesApiService = new OsPlacesApiService(client, config);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => osPlacesApiService.FindFromLpiDataset(query, 1.1));
        }
    }
}


