using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Postcode.Queries.GetBulkPostcodes;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Location.Infrastructure.ApiClient;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Location.Application.UnitTests.Postcode.Queries;

public class WhenHandlingGetBulkPostcodesQuery
{
    [Test, MoqAutoData]
    public async Task Then_The_Api_Is_Called_And_Postcode_Data_Returned(
        List<PostcodeData> data,
        GetBulkPostcodesQuery query,
        [Frozen]Mock<IPostcodeApiService> postcodeApiService,
        GetBulkPostcodesQueryHandler handler)
    {
        postcodeApiService.Setup(x => x.GetBulkPostCodeData(It.Is<GetBulkPostcodeRequest>(c=> c.Postcodes == query.Postcodes))).ReturnsAsync(data);

        var actual = await handler.Handle(query, CancellationToken.None);

        actual.PostCodes.Should().BeEquivalentTo(data);
    }
}