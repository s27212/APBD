using MedApp.DTO;
using MedApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedApp.Controllers;

[ApiController]
[Route("api")]
public class MedController : ControllerBase
{
    private readonly IMedService _service;

    public MedController(IMedService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> createPrescription([FromBody] NewPrescriptionForm form)
    {
        if (form.DueDate < form.Date)
        {
            return BadRequest("DueDate must be >= Date");
        }

        if (form.Medicaments.Count > 10)
        {
            return BadRequest("Prescription can contain no more than 10 medicaments.");
        }

        if (! await _service.AllMedicamentsExist(form.Medicaments.Select(m => m.IdMedicament).ToList()))
        {
            return BadRequest("Some of the provide medicaments do not exist.");
        }

        if (!await _service.DoctorExists(form.Doctor))
        {
            return BadRequest("Doctor does not exist.");
        }

        await _service.CreatePrescription(form);

        return Created();
    }
}