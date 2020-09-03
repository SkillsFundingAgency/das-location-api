using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Application.LocationImport.Handlers.ImportLocations
{
    public class ImportDataCommandHandler : IRequestHandler<ImportDataCommand, Unit>
    {
        private readonly ILocationImportService _importService;

        public ImportDataCommandHandler (ILocationImportService importService)
        {
            _importService = importService;
        }
        public async Task<Unit> Handle(ImportDataCommand request, CancellationToken cancellationToken)
        {
            await _importService.Import();
            
            return Unit.Value;
        }
    }
}