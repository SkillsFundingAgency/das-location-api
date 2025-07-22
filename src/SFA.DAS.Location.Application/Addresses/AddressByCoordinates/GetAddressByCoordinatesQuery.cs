namespace SFA.DAS.Location.Application.Addresses.AddressByCoordinates;

public sealed record GetAddressByCoordinatesQuery(double Latitude, double Longitude, int Radius = 50)
    : MediatR.IRequest<GetAddressByCoordinatesQueryResult>;
