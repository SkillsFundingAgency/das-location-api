using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Location.Domain.Configuration;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Location.Infrastructure.ApiClient;

namespace SFA.DAS.Location.Infrastructure.UnitTests.ApiClient;

public class WhenGettingBulkPostCodeData
{
    [Test, AutoData]
    public async Task Then_The_Endpoint_Is_Called_And_Postcode_Data_Returned(
        PostcodesBulkLocationApiResponse postcodeResponse,
        List<string> postcodes)
    {
        var response = new HttpResponseMessage
        {
            Content = new StringContent(JsonConvert.SerializeObject(postcodeResponse)),
            StatusCode = HttpStatusCode.Accepted,
        };
        var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(Constants.BulkPostcodeUrl), HttpMethod.Post);
        var client = new HttpClient(httpMessageHandler.Object);
        var postcodeService = new PostcodeApiService(client);

        //Act
        var actual = await postcodeService.GetBulkPostCodeData(new GetBulkPostcodeRequest{Postcodes = postcodes});

        //Assert
        actual.Should().BeEquivalentTo(postcodeResponse.Result, options => options.ExcludingMissingMembers());
        
    }
}