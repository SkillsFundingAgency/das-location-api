using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Domain.UnitTests.Models
{
    public class WhenMappingPostcodeApiItemToSuggestedLocationModel
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(PostcodesLocationApiItem source)
        {
            //Act
            var actual = (SuggestedLocation) source;

            //Assert
            actual.Should().BeEquivalentTo(source, options=>options
                .Excluding(x=>x.AdminDistrict));
            actual.AdminDistrict.Should().BeEmpty();
        }
    }
}
