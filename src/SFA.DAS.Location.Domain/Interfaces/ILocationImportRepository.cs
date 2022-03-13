using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Entities;

namespace SFA.DAS.Location.Domain.Interfaces
{
    public interface ILocationImportRepository
    {
        void DeleteAll();
        Task InsertMany(IEnumerable<LocationImport> items);
        Task<IEnumerable<LocationImport>> GetAll();
        Task RunDataLoad();
    }
}