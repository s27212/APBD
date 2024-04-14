using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("/api/visits")]
[ApiController]
public class VisitController : ControllerBase
{
    private static readonly List<Visit> Visits = [
        new Visit { DateTime = new DateTime(2024, 4, 14, 9, 0, 0), AnimalId = 1, Description = "Regular checkup", Price = 50.0 },
        new Visit { DateTime = new DateTime(2024, 4, 15, 10, 30, 0), AnimalId = 2, Description = "Vaccination", Price = 30.0 },
        new Visit { DateTime = new DateTime(2024, 4, 16, 14, 15, 0), AnimalId = 1, Description = "Surgery", Price = 200.0 },
        new Visit { DateTime = new DateTime(2024, 4, 17, 11, 0, 0), AnimalId = 3, Description = "Dental cleaning", Price = 80.0 },
        new Visit { DateTime = new DateTime(2024, 4, 18, 16, 45, 0), AnimalId = 2, Description = "X-ray", Price = 120.0 },
        new Visit { DateTime = new DateTime(2024, 4, 19, 13, 20, 0), AnimalId = 3, Description = "Ultrasound", Price = 150.0 },
    ];

    [HttpGet]
    public IActionResult GetAnimalVisit(int id)
    {
        var visits = Visits.FindAll(visit => visit.AnimalId == id);
        return Ok(visits);
    }

    [HttpPost]
    public IActionResult CreateVisit(Visit visit)
    {
        Visits.Add(visit);
        return NoContent();
    }

}