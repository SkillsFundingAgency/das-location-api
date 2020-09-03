using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.Location.Domain.Entities;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Data.Repository
{
    public class ImportAuditRepository : IImportAuditRepository
    {
        private readonly ILocationDataContext _dataContext;

        public ImportAuditRepository (ILocationDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task Insert(ImportAudit importAudit)
        {
            await _dataContext.ImportAudit.AddAsync(importAudit);
            _dataContext.SaveChanges();
        }

        public async Task<ImportAudit> GetLastImportByType(ImportType importType)
        {
            var record = await _dataContext
                .ImportAudit
                .OrderByDescending(c => c.TimeStarted)
                .FirstOrDefaultAsync(c => c.ImportType.Equals(importType));

            return record;
        }
    }
}