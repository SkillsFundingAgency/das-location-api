using System.Threading;
using System.Threading.Tasks;
using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Domain.Interfaces;

public interface IPostcodeApiV2Service
{
    Task<PostcodeDataV2> LookupPostcodeAsync(string query, CancellationToken cancellationToken);
}