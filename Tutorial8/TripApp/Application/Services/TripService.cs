using TripApp.Application.DTO;
using TripApp.Application.Mappers;
using TripApp.Application.Repositories;
using TripApp.Entities;

namespace TripApp.Application.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _repository;

    public TripService(ITripRepository context)
    {
        _repository = context;
    }

    public async Task<IEnumerable<GetTripDTO>> GetAllTripsAsync()
    {
        var trips = await _repository.GetAllTripsAsync();
        return trips.Select(trip => trip.MapToGetTripDto());
    }

    public async Task<PagedList<GetTripDTO>> GetPaginatedTripsAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 10) pageSize = 10;
        var result = await _repository.GetPaginatedTripsAsync(page, pageSize);
        
        var mappedTrips = new PagedList<GetTripDTO>()
        {
            AllPages = result.AllPages,
            PageNum = result.PageNum,
            PageSize = result.PageSize
        };
        mappedTrips.AddRange(result.Select(trip => trip.MapToGetTripDto()));
        
        return mappedTrips;
    }

    public async Task<IEnumerable<GetTripDTO>> GetClientTripsAsync(int idClient)
    {
        var trips = await _repository.GetClientTrips(idClient);
        return trips.Select(trip => trip.MapToGetTripDto());
    }

    public async Task DeleteClientAsync(int idClient)
    {
        await _repository.DeleteClientAsync(idClient);
    }

    public async Task<bool> ClientExistsAsync(string pesel)
    {
        return await _repository.ClientExistsAsync(pesel);
    }

    public async Task<bool> ClientIsAssignedToTripAsync(string pesel, int idTrip)
    {
        return await _repository.ClientIsAssignedToTripAsync(pesel, idTrip);
    }

    public async Task<GetTripDTO?> GetTripByIdAsync(int idTrip)
    {
        var trip = await _repository.GetTripByIdAsync(idTrip);
        return trip?.MapToGetTripDto();
    }

    public async Task AssignClientToTripAsync(int idTrip, RegisterClientDTO clientDto)
    {
        var client = clientDto.MapToClientEntity();
        
        await _repository.AssignClientToTripAsync(idTrip, client, clientDto.PaymentDate);
    }
}