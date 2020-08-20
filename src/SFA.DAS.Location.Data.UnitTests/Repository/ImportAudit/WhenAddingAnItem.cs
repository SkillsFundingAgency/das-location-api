using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.Location.Data.UnitTests.Repository.ImportAudit
{
    public class WhenAddingAnItem
    {
        private Mock<ILocationDataContext> _locationDataContext;
        private Domain.Entities.ImportAudit _importAudit;
        private Data.Repository.ImportAuditRepository _importAuditRepository;
        private readonly DateTime _expectedDate = new DateTime(2020,10,30);
        private const int ExpectedRowsImported = 100;
        
        [SetUp]
        public void Arrange()
        {
            _importAudit = new Domain.Entities.ImportAudit(_expectedDate, 100);
            
            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.ImportAudit).ReturnsDbSet(new List<Domain.Entities.ImportAudit>());
            _importAuditRepository = new Data.Repository.ImportAuditRepository(_locationDataContext.Object);
        }

        [Test]
        public async Task Then_The_Import_Audit_Record_Is_Added()
        {
            //Act
            await _importAuditRepository.Insert(_importAudit);
            
            //Assert
            _locationDataContext.Verify(x=>
                    x.ImportAudit.AddAsync(
                        It.Is<Domain.Entities.ImportAudit>(c=>c.TimeStarted.Equals(_expectedDate)
                                                              && c.RowsImported.Equals(ExpectedRowsImported))
                        ,It.IsAny<CancellationToken>())
                , Times.Once);
            _locationDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}