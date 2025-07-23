using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Location.Infrastructure.ApiClient;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient;
[TestFixture]
public class WhenGettingNearestFromDpaDataset
{
    private const string ApiKey = "test-key";
    private readonly LocationApiConfiguration _config = new() { OsPlacesApiKey = ApiKey };
    private const string Query = "test-query";

    private HttpClient CreateHttpClient(HttpStatusCode statusCode, string content)
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            });
        return new HttpClient(handlerMock.Object);
    }

    [Test]
    public async Task NearestFromDpaDataset_ReturnsEmptySuggestedPlace_WhenNotFound()
    {
        var client = CreateHttpClient(HttpStatusCode.NotFound, "");
        var sut = new OsPlacesApiService(client, _config);

        var result = await sut.NearestFromDpaDataset(Query);

        result.Should().NotBeNull();
        result.Should().BeOfType<SuggestedPlace>();
        result.Uprn.Should().BeNull();
    }

    [Test]
    public async Task NearestFromDpaDataset_ReturnsEmptySuggestedPlace_WhenResultsNullOrEmpty()
    {
        var response = JsonConvert.SerializeObject(new OsNearestApiResponse { Results = null });
        var client = CreateHttpClient(HttpStatusCode.OK, response);
        var sut = new OsPlacesApiService(client, _config);

        var result = await sut.NearestFromDpaDataset(Query);

        result.Should().NotBeNull();
        result.Should().BeOfType<SuggestedPlace>();
        result.Uprn.Should().BeNull();

        response = JsonConvert.SerializeObject(new OsNearestApiResponse { Results = new List<OsNearestApiResponse.Result>() });
        client = CreateHttpClient(HttpStatusCode.OK, response);
        sut = new OsPlacesApiService(client, _config);

        result = await sut.NearestFromDpaDataset(Query);

        result.Should().NotBeNull();
        result.Should().BeOfType<SuggestedPlace>();
        result.Uprn.Should().BeNull();
    }

    [Test, MoqAutoData]
    public async Task NearestFromDpaDataset_ReturnsSuggestedPlace_WhenResultExists(OsNearestApiResponse.Dpa dpa)
    {
        dpa.Countrycode = "E";
        var responseObj = new OsNearestApiResponse
        {
            Results = [new OsNearestApiResponse.Result {Dpa = dpa}]
        };
        var response = JsonConvert.SerializeObject(responseObj);
        var client = CreateHttpClient(HttpStatusCode.OK, response);
        var sut = new OsPlacesApiService(client, _config);

        var result = await sut.NearestFromDpaDataset(Query);

        result.Should().NotBeNull();
        result.Uprn.Should().Be(dpa.Uprn);
        result.BuildingName.Should().Be(dpa.Organisationname);
        result.AddressLine1.Should().Be(dpa.Buildingname);
        result.AddressLine2.Should().Be(dpa.Thoroughfarename);
        result.AddressLine3.Should().Be(dpa.Posttown);
        result.Longitude.Should().Be(dpa.Lng);
        result.Latitude.Should().Be(dpa.Lat);
        result.Postcode.Should().Be(dpa.Postcode);
    }
}