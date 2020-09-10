using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Domain.UnitTests.Models
{
    public class WhenMappingLocationToSuggestedLocationModel
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Domain.Entities.Location source)
        {
            //Arrange & Act
            var actual = (SuggestedLocation)source;

            //Assert
            actual.Should().BeEquivalentTo(source, options => options.ExcludingMissingMembers());
        }
    }
}
