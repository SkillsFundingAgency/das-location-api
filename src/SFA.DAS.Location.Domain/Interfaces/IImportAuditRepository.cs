using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Entities;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface IImportAuditRepository
    {
        Task Insert(ImportAudit importAudit);
        Task<ImportAudit> GetLastImportByType(ImportType importType);
    }
}