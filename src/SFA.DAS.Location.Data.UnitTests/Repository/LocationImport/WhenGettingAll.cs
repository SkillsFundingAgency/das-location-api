using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.Location.Data.UnitTests.Repository.LocationImport
{
    public class WhenGettingAll
    {
        private Mock<ILocationDataContext> _locationDataContext;
        private List<Domain.Entities.LocationImport> _items;
        private Data.Repository.LocationImportRepository _locationImportRepository;

        [SetUp]
        public void Arrange()
        {
            _items = new List<Domain.Entities.LocationImport>
            {
                new Domain.Entities.LocationImport
                {
                    Id = 1
                },
                new Domain.Entities.LocationImport
                {
                    Id = 1
                }
            };

            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.LocationImports).ReturnsDbSet(_items);


            _locationImportRepository = new Data.Repository.LocationImportRepository(_locationDataContext.Object);
        }

        [Test]
        public async Task Then_The_Items_Are_Returned()
        {
            var actual = await _locationImportRepository.GetAll();

            actual.Should().BeEquivalentTo(_items);

        }
    }
}