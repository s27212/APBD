using TripApp.Application.DTO;
using TripApp.Entities;

namespace TripApp.Application.Mappers;

public static class ClientMapper
{
    public static ClientDTO MapToClientDto(this Client client)
    {
        return new ClientDTO()
        {
            FirstName = client.FirstName,
            LastName = client.LastName
        };
    }

    public static Client MapToClientEntity(this RegisterClientDTO dto)
    {
        return new Client()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel
        };
    }
}