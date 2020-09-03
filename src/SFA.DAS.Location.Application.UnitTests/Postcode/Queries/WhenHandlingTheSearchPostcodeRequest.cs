using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Location.Queries.SearchLocations;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Postcode.Queries
{
    class WhenHandlingTheSearchPostcodeRequest
    {
        [Test, MoqAutoData]
        public async Task Then_The_Postcode_Service_Is_Called(
            GetLocationsQuery query,
            List<SuggestedLocation> postcodes,
            [Frozen] Mock<IPostcodeService> service,
            GetLocationsQueryHandler handler
            )
        {
            //Arrange
            service.Setup(x => x.GetPostcodeByOutcodeQuery(query.Query, query.ResultCount)).ReturnsAsync(postcodes);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);

            //Assert
            actual.SuggestedLocations.Equals(postcodes);
        }

    }
}
