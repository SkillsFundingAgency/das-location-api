using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Domain.Entities;
using SFA.DAS.Location.Domain.ImportTypes;

namespace SFA.DAS.Location.Domain.UnitTests.Entities
{
    public class WhenMappingLocationApiItemToLocationImport
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Attributes source)
        {
            var actual = (LocationImport) source;
            
            actual.Should().BeEquivalentTo(source);
        }
    }
}