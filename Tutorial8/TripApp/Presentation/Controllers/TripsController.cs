using Microsoft.AspNetCore.Mvc;
using TripApp.Application.DTO;
using TripApp.Application.Services;

namespace TripApp.Presentation.Controllers;

[ApiController]
[Route("/api/")]
public class TripsController : ControllerBase
{
    private readonly ITripService _service;

    public TripsController(ITripService service)
    {
        _service = service;
    }

    [HttpGet("/trips")]
    public async Task<IActionResult> GetTrips(int? page, int? pageSize)
    {
        if (page is null || pageSize is null)
            return Ok(await _service.GetAllTripsAsync());

        return Ok(await _service.GetPaginatedTripsAsync(page.Value, pageSize.Value));
    }

    [HttpDelete("/clients/{idClient:int}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        if ((await _service.GetClientTripsAsync(idClient)).Any())
        {
            return BadRequest("The client has trips assigned to them.");
        }

        await _service.DeleteClientAsync(idClient);
        return NoContent();
    }

    [HttpPost("/trips/{idTrip:int}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] RegisterClientDTO dto)
    {
        var tripDto = await _service.GetTripByIdAsync(idTrip);
        if (tripDto == null)
        {
            return BadRequest("Trip with a given id does not exist.");
        }

        if (tripDto.DateFrom < DateTime.Now)
        {
            return BadRequest("The trip with a given id has already happened.");
        }

        if (await _service.ClientExistsAsync(dto.Pesel))
        {
            if (await _service.ClientIsAssignedToTripAsync(dto.Pesel, idTrip))
            {
                return BadRequest("Client with a given PESEL is already registered for this trip.");
            }
            return BadRequest("Client with a given PESEL already exists.");
        }

        await _service.AssignClientToTripAsync(idTrip, dto);
        return Created();
    }
    
}