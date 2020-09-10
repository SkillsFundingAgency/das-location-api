using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Domain.ImportTypes;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Domain.UnitTests.Models
{
    public class WhenMappingPostcodeApiItemToPostcodeDataModel
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(PostcodesLocationApiItem source)
        {
            //Arrange & Act
            var actual = (PostcodeData)source;

            //Assert
            actual.Should().BeEquivalentTo(source, options => options.ExcludingMissingMembers());
        }
    }
}
