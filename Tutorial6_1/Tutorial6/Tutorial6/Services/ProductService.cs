using System.Data;
using Microsoft.Data.SqlClient;
using Tutorial6.Models;

namespace Tutorial6.Repositories;

public class ProductService : IProductService
{
    private readonly IConfiguration _configuration;

    public ProductService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<decimal> AddProductAsync(WarehouseProductDTO productDto)
    {
        var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await con.OpenAsync();
        var command = new SqlCommand()
        {
            CommandText = "SELECT 1 FROM Product WHERE @IdProduct = IdProduct",
            Connection = con,
        };
        command.Parameters.AddWithValue("@IdProduct", productDto.IdProduct);

        await using (var reader = await command.ExecuteReaderAsync())
        {
            if (!reader.HasRows)
            {
                throw new ArgumentException("Product with provided id does not exist.");
            }
        }

        command.Parameters.AddWithValue("@IdWarehouse", productDto.IdWarehouse);
        command.CommandText = "SELECT 1 FROM Warehouse WHERE @IdWarehouse = IdWarehouse";

        await using (var reader = await command.ExecuteReaderAsync())
        {
            if (!reader.HasRows)
            {
                throw new ArgumentException("Warehouse with provided id does not exist.");
            }
        }

        command.CommandText = "SELECT * FROM \"Order\" o " +
                              "LEFT JOIN Product_Warehouse pw ON o.IdOrder = pw.IdOrder " +
                              "WHERE @IdProduct = o.IdProduct " +
                              "AND @Amount = o.Amount AND o.FulfilledAt IS NULL AND pw.IdProductWarehouse IS NULL " +
                              "AND o.CreatedAt < @CreatedAt";
        command.Parameters.AddWithValue("@Amount", productDto.Amount);
        command.Parameters.AddWithValue("@CreatedAt", productDto.CreatedAt);
        OrderDTO order;
        await using (var reader = await command.ExecuteReaderAsync())
        {
            if (!reader.HasRows)
            {
                throw new ArgumentException("The order for given product does not exist.");
            }

            reader.Read();
            order = new OrderDTO()
            {
                IdOrder = (int)reader["IdOrder"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                FulfilledAt = await reader.IsDBNullAsync(4) ? null : (DateTime?)reader["FulfilledAt"]
            };
        }
        
        await using var tran = await con.BeginTransactionAsync();

        command.CommandText = "UPDATE \"Order\" SET FulfilledAt = @CreatedAt WHERE IdOrder = @IdOrder";
        command.Parameters.AddWithValue("@IdOrder", order.IdOrder);
        command.Transaction = (SqlTransaction)tran;
        await command.ExecuteNonQueryAsync();
        command.CommandText = "SELECT Price FROM Product WHERE IdProduct = @IdProduct";
        var price = (decimal)await command.ExecuteScalarAsync();
        command.CommandText =
            "INSERT INTO Product_Warehouse VALUES(@IdProduct, @IdWarehouse, @IdOrder, @Amount, @Price, @CreatedAt);" +
            "SELECT SCOPE_IDENTITY()";
        command.Parameters.AddWithValue("@Price", price * productDto.Amount);
        var pk = (decimal)await command.ExecuteScalarAsync();

        await tran.CommitAsync();
        return pk;
    }

    public async Task<decimal> AddProductProcedureAsync(WarehouseProductDTO productDto)
    {
        await using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await con.OpenAsync();

        var command = new SqlCommand("AddProductToWarehouse", con);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@IdProduct", productDto.IdProduct);
        command.Parameters.AddWithValue("@IdWarehouse", productDto.IdWarehouse);
        command.Parameters.AddWithValue("@Amount", productDto.Amount);
        command.Parameters.AddWithValue("@CreatedAt", productDto.CreatedAt);

        var newProductId = (decimal)await command.ExecuteScalarAsync();

        return newProductId;
    }
}