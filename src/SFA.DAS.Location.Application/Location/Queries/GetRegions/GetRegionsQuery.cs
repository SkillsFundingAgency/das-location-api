using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Location.Domain.Interfaces;

namespace SFA.DAS.Location.Application.Location.Queries.GetRegions
{
    public class GetRegionsQuery: IRequest<GetRegionsQueryResult>
    {
    }
}