using TripApp.Application.DTO;
using TripApp.Entities;

namespace TripApp.Application.Services;

public interface ITripService
{
    Task<IEnumerable<GetTripDTO>> GetAllTripsAsync();
    Task<PagedList<GetTripDTO>> GetPaginatedTripsAsync(int page, int pageSize);
    Task<IEnumerable<GetTripDTO>> GetClientTripsAsync(int idClient);
    Task DeleteClientAsync(int idClient);
    Task<bool> ClientExistsAsync(string pesel);
    Task<bool> ClientIsAssignedToTripAsync(string pesel, int idTrip);
    Task<GetTripDTO?> GetTripByIdAsync(int idTrip);
    Task AssignClientToTripAsync(int idTrip, RegisterClientDTO clientDto);
}