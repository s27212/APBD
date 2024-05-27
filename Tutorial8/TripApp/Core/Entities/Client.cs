﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripApp.Entities;

[Table("Client")]
public partial class Client
{
    [Key]
    public int IdClient { get; set; }

    [StringLength(120)]
    public string FirstName { get; set; } = null!;

    [StringLength(120)]
    public string LastName { get; set; } = null!;

    [StringLength(120)]
    public string Email { get; set; } = null!;

    [StringLength(120)]
    public string Telephone { get; set; } = null!;

    [StringLength(120)]
    public string Pesel { get; set; } = null!;

    [InverseProperty("IdClientNavigation")]
    public virtual ICollection<Client_Trip> ClientTrips { get; set; } = new List<Client_Trip>();
}
