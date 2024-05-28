using TripApp.Entities;

namespace TripApp.Application.Repositories;

public interface ITripRepository
{
    Task<IEnumerable<Trip>> GetAllTripsAsync();
    Task<PagedList<Trip>> GetPaginatedTripsAsync(int page, int pageSize);
    Task<IEnumerable<Trip>> GetClientTrips(int idClient);
    Task DeleteClientAsync(int idClient);
    Task<bool> ClientExistsAsync(string pesel);
    Task<bool> ClientIsAssignedToTripAsync(string pesel, int idTrip);
    Task<Trip?> GetTripByIdAsync(int idTrip);
    Task AssignClientToTripAsync(int idTrip, Client client, DateTime? paymentDate);
}