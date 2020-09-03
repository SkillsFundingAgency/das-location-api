using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Domain.Entities;

namespace SFA.DAS.Location.Domain.UnitTests.Entities
{
    public class WhenMappingLocationImportToLocation
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(LocationImport source)
        {
            var actual = (Domain.Entities.Location) source;
            
            actual.Should().BeEquivalentTo(source);
        }
    }
}