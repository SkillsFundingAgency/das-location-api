using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Postcode.Queries.GetByFullPostcode;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Postcode.Queries
{
    class WhenHandlingTheSearchPostcodeRequest
    {
        [Test, MoqAutoData]
        public async Task Then_The_Postcode_Service_Is_Called(
            GetPostcodeQuery query,
            PostcodeData postcode,
            [Frozen] Mock<IPostcodeService> service,
            GetPostcodeQueryHandler handler
            )
        {
            //Arrange
            service.Setup(x => x.GetPostcodeByFullPostcode(query.Postcode)).ReturnsAsync(postcode);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);

            //Assert
            actual.Postcode.Should().Be(postcode);
        }

    }
}
