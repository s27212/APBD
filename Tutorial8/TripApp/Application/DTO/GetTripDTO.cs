﻿namespace TripApp.Application.DTO;

public class GetTripDTO
{
    public required string Name { get; set; } = null!;
    
    public required string Description { get; set; } = null!;
    
    public required DateTime DateFrom { get; set; }
    
    public required DateTime DateTo { get; set; }

    public required int MaxPeople { get; set; }
    
    public required List<ClientDTO> Clients { get; set; } = [];
    
    public required List<CountryDTO> Countries { get; set; } = [];
}