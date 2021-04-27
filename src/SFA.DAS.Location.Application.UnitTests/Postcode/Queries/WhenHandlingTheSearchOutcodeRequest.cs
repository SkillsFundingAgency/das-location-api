using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Postcode.Queries.GetByOutcode;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Postcode.Queries
{
    public class WhenHandlingTheSearchOutcodeRequest
    {
        [Test, MoqAutoData]
        public async Task Then_The_Postcode_Service_Is_Called(
            GetOutcodeQuery query,
            SuggestedLocation postcode,
            [Frozen] Mock<IPostcodeService> service,
            GetOutcodeQueryHandler handler
            )
        {
            //Arrange
            service.Setup(x => x.GetDistrictNameByOutcodeQuery(query.Outcode)).ReturnsAsync(postcode);

            //Act
            var actual = await handler.Handle(query, CancellationToken.None);

            //Assert
            actual.Outcode.Should().Be(postcode);
        }
    }
}
