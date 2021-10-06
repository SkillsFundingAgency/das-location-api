using MediatR;

namespace SFA.DAS.Location.Application.Addresses.Queries
{
    public class GetAddressesQuery : IRequest<GetAddressesQueryResult>
    {
        public string Query { get ; set ; }
        public double MinMatch { get; set; }
    }
}