using TripApp.Application.DTO;
using TripApp.Entities;

namespace TripApp.Application.Mappers;

public static class TripMapper
{
    public static GetTripDTO MapToGetTripDto(this Trip trip)
    {
        return new GetTripDTO()
        {
            Name = trip.Name,
            DateFrom = trip.DateFrom,
            DateTo = trip.DateTo,
            Description = trip.Description,
            MaxPeople = trip.MaxPeople,
            Countries = trip.IdCountries.Select(country => country.MapToCountryDto()).ToList(),
            Clients = trip.ClientTrips.Select(e => e.IdClientNavigation.MapToClientDto()).ToList()
        };
    }
}