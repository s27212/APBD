using TripApp.Application.DTO;
using TripApp.Entities;

namespace TripApp.Application.Mappers;

public static class CountryMapper
{
    public static CountryDTO MapToCountryDto(this Country country)
    {
        return new CountryDTO()
        {
            Name = country.Name
        };
    }   
}