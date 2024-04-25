using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalController : ControllerBase
{
    private IAnimalRepository _repository;

    public AnimalController(IAnimalRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "Name")
    {
        if (orderBy != "Name" && orderBy != "Description" && orderBy != "Area" && orderBy != "Category")
        {
            return BadRequest($"{orderBy} is an incorrect parameter.");
        }

        var animals = _repository.GetAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        _repository.AddAnimal(animal);
        return Created();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        _repository.DeleteAnimal(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        _repository.UpdateAnimal(id, animal);
        return NoContent();
    }

}