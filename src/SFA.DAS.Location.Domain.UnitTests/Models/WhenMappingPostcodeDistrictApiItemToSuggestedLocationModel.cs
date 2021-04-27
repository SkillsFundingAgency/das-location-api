using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Domain.UnitTests.Models
{
    public class WhenMappingPostcodeDistrictApiItemToSuggestedLocationModel
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(PostcodeDistrictLocationApiResponse source)
        {
            //Arrange & Act
            var actual = (SuggestedLocation)source;

            //Assert
            actual.AdminDistrict.Should().BeEquivalentTo(source.AdminDistrict[0]);
            actual.Country.Should().BeEquivalentTo(source.Country[0]);

            actual.Lat.Should().Be(source.Lat);
            actual.Long.Should().Be(source.Long);
            actual.Postcode.Should().Be(source.Postcode);
            actual.Outcode.Should().Be(source.Outcode);



        }
    }
}
