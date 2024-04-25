using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimals()
    {
        return GetAnimals("Name");
    }

    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        using var con =
            new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT * FROM Animal ORDER BY " +
                              "CASE WHEN @OrderBy = 'Description' THEN Description\n" +
                              "WHEN @OrderBy = 'Area' THEN Area\n" +
                              "WHEN @OrderBy = 'Category' THEN Category\n" +
                              "ELSE Name END"; 
        command.Parameters.AddWithValue("@OrderBy", orderBy);

        var dr = command.ExecuteReader();
        var animals = new List<Animal>();
        
        while (dr.Read())
        {
            var animal = new Animal
            {
                IdAnimal = (int)dr["IdAnimal"],
                Name = dr["Name"].ToString(),
                Category = dr["Category"].ToString(),
                Description = dr["Description"].ToString(),
                Area = dr["Area"].ToString()
            };
            animals.Add(animal);
        }

        return animals;
    }

    public int AddAnimal(Animal animal)
    {
        using var con =
            new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "INSERT INTO Animal(Name, Description, Category, Area) " +
                              "VALUES(@Name, @Description, @Category, @Area)";
        command.Parameters.AddWithValue("Name", animal.Name);
        command.Parameters.AddWithValue("Description", animal.Description);
        command.Parameters.AddWithValue("Category", animal.Category);
        command.Parameters.AddWithValue("Area", animal.Area);

        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }

    public int DeleteAnimal(int id)
    {
        using var con =
            new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @id";
        command.Parameters.AddWithValue("Id", id);
        
        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }

    public int UpdateAnimal(int id, Animal animal)
    {
        using var con =
            new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, " +
                              "Area = @Area WHERE idAnimal = @Id ";
        command.Parameters.AddWithValue("Name", animal.Name);
        command.Parameters.AddWithValue("Description", animal.Description);
        command.Parameters.AddWithValue("Category", animal.Category);
        command.Parameters.AddWithValue("Area", animal.Area);
        command.Parameters.AddWithValue("Id", id);

        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }
}