using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Application.Location.Services;
using SFA.DAS.Location.Application.Postcode.Services;
using SFA.DAS.Location.Domain.Interfaces;
using SFA.DAS.Location.Domain.Models;
using SFA.DAS.Location.Infrastructure.ApiClient;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Location.Application.UnitTests.Postcode.Services
{
    public class WhenGettingLocationsByOutcode
    {
        [Test, MoqAutoData]
        public async Task Then_The_Api_Is_Called_With_The_Query_And_Count(
            int resultCount,
            string query,
            IEnumerable<SuggestedLocation> locations,
            [Frozen] Mock<IPostcodeApiService> apiService,
            PostcodeService service
            )
        {
            //Arrange
            apiService.Setup(x => x.GetAllStartingWithOutcode(query, resultCount))
                .ReturnsAsync(locations);

            //Act
            var actual = await service.GetPostcodeByOutcodeQuery(query, resultCount);

            //Assert
            actual.Should().BeEquivalentTo(locations);
        }
    }
}
