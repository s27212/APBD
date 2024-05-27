using Microsoft.EntityFrameworkCore;
using TripApp.Application.Repositories;
using TripApp.Context;
using TripApp.Entities;

namespace TripApp.Infrastructure.Repositories;

public class TripRepository : ITripRepository
{
    private readonly TripDbContext _context;

    public TripRepository(TripDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trip>> GetAllTripsAsync()
    {
        return await _context.Trips.Include(e => e.ClientTrips)
            .ThenInclude(e => e.IdClientNavigation)
            .Include(e => e.IdCountries)
            .OrderBy(e => e.DateFrom)
            .ToListAsync();
    }

    public async Task<PagedList<Trip>> GetPaginatedTripsAsync(int page, int pageSize)
    {
        var allTrips = await GetAllTripsAsync();
        var tripsCount = allTrips.Count();
        var totalPages = tripsCount / pageSize;
        var trips = await _context.Trips
            .Include(e => e.ClientTrips).ThenInclude(e => e.IdClientNavigation)
            .Include(e => e.IdCountries)
            .OrderBy(e => e.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var pagedList = new PagedList<Trip>()
        {
            AllPages = totalPages,
            PageSize = pageSize,
            PageNum = page
        };
        pagedList.AddRange(trips);
        return pagedList;
    }

    public async Task<IEnumerable<Trip>> GetClientTrips(int idClient)
    {
        return await _context.Trips.Include(e => e.ClientTrips)
            .ThenInclude(e => e.IdClientNavigation)
            .Include(e => e.IdCountries)
            .Where(e => e.ClientTrips.Any(clientTrip => clientTrip.IdClientNavigation.IdClient == idClient))
            .ToListAsync();
    }

    public async Task DeleteClientAsync(int idClient)
    {
        await _context.Clients.Where(e => e.IdClient == idClient).ExecuteDeleteAsync();
    }

    public async Task<bool> ClientExistsAsync(string pesel)
    {
        return await _context.Clients.AnyAsync(e => e.Pesel == pesel);
    }

    public async Task<bool> ClientIsAssignedToTripAsync(string pesel, int idTrip)
    {
        return await _context.Client_Trips.AnyAsync(e => e.IdTrip == idTrip && e.IdClientNavigation.Pesel == pesel);
    }

    public async Task<Trip?> GetTripByIdAsync(int idTrip)
    {
        return await _context.Trips
            .Include(e => e.ClientTrips)
            .ThenInclude(e => e.IdClientNavigation)
            .Include(e => e.IdCountries)
            .FirstOrDefaultAsync(e => e.IdTrip == idTrip);
    }
    

    public async Task AssignClientToTripAsync(int idTrip, Client client, DateTime? paymentDate)
    {
        var newClient = (await _context.Clients.AddAsync(client)).Entity;
        var clientTrip = new Client_Trip()
        {
            IdClient = newClient.IdClient,
            IdClientNavigation = newClient,
            IdTrip = idTrip,
            IdTripNavigation = (await GetTripByIdAsync(idTrip))!,
            RegisteredAt = DateTime.Now,
            PaymentDate = paymentDate
        };
        await _context.Client_Trips.AddAsync(clientTrip);
        await _context.SaveChangesAsync();
    }
}