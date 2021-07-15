using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Location.Data.UnitTests.DatabaseMock;
using SFA.DAS.Location.Domain.Entities;

namespace SFA.DAS.Location.Data.UnitTests.Repository.ImportAudit
{
    public class WhenGettingAnItemByImportType
    {
        private Mock<ILocationDataContext> _locationDataContext;
        private List<Domain.Entities.ImportAudit> _importAudits;
        private Data.Repository.ImportAuditRepository _importAuditRepository;
        
        [SetUp]
        public void Arrange()
        {
            _importAudits = new List<Domain.Entities.ImportAudit>
            {
                new Domain.Entities.ImportAudit(new DateTime(2020,10,01), 200, "test"),
                new Domain.Entities.ImportAudit(new DateTime(2020,09,30), 100, "")
            };
            
            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.ImportAudit).ReturnsDbSet(_importAudits);
            

            _importAuditRepository = new Data.Repository.ImportAuditRepository(_locationDataContext.Object);
        }

        [Test]
        public async Task Then_The_Latest_ImportAudit_Record_Is_Returned()
        {
            //Act
            var auditRecord = await _importAuditRepository.GetLastImportByType(ImportType.OnsLocation);
            
            //Assert
            Assert.IsNotNull(auditRecord);
            auditRecord.RowsImported.Should().Be(200);
            auditRecord.Name.Should().Be("test");
        }

        [Test]
        public async Task Then_No_Item_Returns_Null()
        {
            //Arrange
            _importAudits = new List<Domain.Entities.ImportAudit>();
            
            _locationDataContext = new Mock<ILocationDataContext>();
            _locationDataContext.Setup(x => x.ImportAudit).ReturnsDbSet(_importAudits);
            _importAuditRepository = new Data.Repository.ImportAuditRepository(_locationDataContext.Object);
            
            //Act
            var auditRecord = await _importAuditRepository.GetLastImportByType(ImportType.OnsLocation);
            
            //Assert
            Assert.IsNull(auditRecord);
        }
    }
}