using SFA.DAS.Location.Domain.Models;
using System.Collections.Generic;

namespace SFA.DAS.Location.Application.Addresses.Queries
{
    public class GetAddressesQueryResult
    {
        public IEnumerable<SuggestedAddress> Addresses { get; set; }
    }
}