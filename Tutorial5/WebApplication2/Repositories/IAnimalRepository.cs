using WebApplication2.Models;

namespace WebApplication2.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals();
    IEnumerable<Animal> GetAnimals(string orderBy);
    int AddAnimal(Animal animal);
    int DeleteAnimal(int id);
    int UpdateAnimal(int id, Animal animal);
}