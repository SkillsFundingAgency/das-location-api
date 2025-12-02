using SFA.DAS.Location.Domain.Models;

namespace SFA.DAS.Location.Application.Addresses.AddressByCoordinates;

public sealed record GetAddressByCoordinatesQueryResult(SuggestedPlace Place);