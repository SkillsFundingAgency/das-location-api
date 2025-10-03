using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using FluentAssertions;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Infrastructure.ApiClient;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient;

public class WhenGettingLookupPostcode
{
    [Test, MoqAutoData]
    public async Task Then_The_Result_Is_Mapped_Correctly(PostcodeLookupResponse postcodeResponse, string query)
    {
        // arrange
        var response = new HttpResponseMessage
        {
            Content = new StringContent(JsonSerializer.Serialize(postcodeResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower })),
            StatusCode = HttpStatusCode.Accepted,
        };
        var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodeUrl, query)));
        var client = new HttpClient(httpMessageHandler.Object);
        var postcodeService = new PostCodeApiV2Service(client);
        
        // act
        var result = await postcodeService.LookupPostcodeAsync(query, CancellationToken.None);

        // assert
        result.Should().BeEquivalentTo(postcodeResponse.Result);
    }
    
    [Test, MoqAutoData]
    public async Task Then_When_The_Latitude_And_Longitude_Are_Null_They_Are_Returned_Correctly(PostcodeLookupResponse postcodeResponse, string query)
    {
        // arrange
        postcodeResponse.Result.Latitude = null;
        postcodeResponse.Result.Longitude = null;

        var response = new HttpResponseMessage
        {
            Content = new StringContent(JsonSerializer.Serialize(postcodeResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower })),
            StatusCode = HttpStatusCode.Accepted,
        };
        var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodeUrl, query)));
        var client = new HttpClient(httpMessageHandler.Object);
        var postcodeService = new PostCodeApiV2Service(client);
        
        // act
        var result = await postcodeService.LookupPostcodeAsync(query, CancellationToken.None);

        // assert
        result.Should().BeEquivalentTo(postcodeResponse.Result);
    }
    
    [Test, MoqAutoData]
    public async Task Then_If_The_Postcode_Is_Not_Found_Null_Is_Returned(PostcodeLookupResponse postcodeResponse, string query)
    {
        // arrange
        var response = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
        var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodeUrl, query)));
        var client = new HttpClient(httpMessageHandler.Object);
        var postcodeService = new PostCodeApiV2Service(client);
        
        // act
        var result = await postcodeService.LookupPostcodeAsync(query, CancellationToken.None);

        // assert
        result.Should().BeNull();
    }
    
    [Test, MoqAutoData]
    public async Task Too_Many_Requests_To_Server_Would_Cause_A_HttpRequestException_To_Be_Thrown(PostcodeLookupResponse postcodeResponse, string query)
    {
        // arrange
        var response = new HttpResponseMessage { StatusCode = HttpStatusCode.TooManyRequests };
        var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(string.Format(Constants.PostcodeUrl, query)));
        var client = new HttpClient(httpMessageHandler.Object);
        var postcodeService = new PostCodeApiV2Service(client);
        
        // act
        var func = async () => await postcodeService.LookupPostcodeAsync(query, CancellationToken.None);

        // assert
        await func.Should().ThrowAsync<HttpRequestException>();
    }
}