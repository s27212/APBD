using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalController : ControllerBase
{
    private static readonly List<Animal> _animals = new()
    {
        new Animal { Id = 1, Name = "Dog", Category = "Dog", Weight = 20, FurColor = "Brown" },
        new Animal { Id = 2, Name = "Bird", Category = "Bird", Weight = 4, FurColor = "Brown" },
        new Animal { Id = 3, Name = "Cat", Category = "Cat", Weight = 7, FurColor = "Gray" }
    };

    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        return animal == null ? NotFound($"Animal with id {id} does not exist.") : Ok(animal);
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        _animals.Add(animal);
        return Created();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, Animal editedAnimal)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} does not exist.");
        }

        _animals.Remove(animal);
        _animals.Add(editedAnimal);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} does not exist.");
        }

        _animals.Remove(animal);
        return NoContent();
    }
}