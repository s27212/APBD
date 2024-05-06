using System.Data;
using Microsoft.Data.SqlClient;
using Tutorial6.Entities;

namespace Tutorial6.Database;

public class Database : IDatabase
{
    private SqlTransaction? _transaction;
    private readonly SqlConnection _connection;

    public Database(IConfiguration configuration)
    {
        _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        _connection.Open();
    }
    
    public async Task<int> AddProductProcedureAsync(NewProductInfo info)
    {
        var cmd = CreateSqlCommand("AddProductToWarehouse");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdProduct", info.ProductId);
        cmd.Parameters.AddWithValue("@IdWarehouse", info.WarehouseId);
        cmd.Parameters.AddWithValue("@Amount", info.Amount);
        cmd.Parameters.AddWithValue("@CreatedAt", info.CreatedAt);
        
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task<bool> ProductExistsAsync(int idProduct)
    {
        await using var command = CreateSqlCommand("SELECT 1 FROM Product WHERE @IdProduct = IdProduct");
        command.Parameters.AddWithValue("@IdProduct", idProduct);
        
        await using var reader = await command.ExecuteReaderAsync();
        return reader.HasRows;
    }

    public async Task<bool> WarehouseExistsAsync(int idWarehouse)
    {
        await using var command = CreateSqlCommand("SELECT 1 FROM Warehouse WHERE @IdWarehouse = IdWarehouse");
        command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
        
        await using var reader = await command.ExecuteReaderAsync();
        return reader.HasRows;
    }

    public async Task<int?> GetPendingOrderAsync(int idProduct, int amount, DateTime createdAt)
    {
        await using var cmd = CreateSqlCommand("SELECT Top 1 o.IdOrder FROM [Order] o " +
                                   "WHERE o.IdProduct = @IdProduct " +
                                       "AND o.Amount = @Amount " +
                                       "AND o.CreatedAt < @CreatedAt " +
                                       "AND NOT EXISTS (SELECT 1 FROM Product_Warehouse WHERE IdOrder = o.IdOrder) " +
                                   "ORDER BY o.CreatedAt ASC");
        cmd.Parameters.AddWithValue("@IdProduct", idProduct);
        cmd.Parameters.AddWithValue("@Amount", amount);
        cmd.Parameters.AddWithValue("@CreatedAt", createdAt);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (reader.Read())
        {
            return (int)reader["IdOrder"];
        }

        return null;
    }

    public async Task<decimal> GetProductPriceAsync(int idProduct)
    {
        await using var cmd = CreateSqlCommand("SELECT Price FROM Product where IdProduct=@IdProduct");
        cmd.Parameters.AddWithValue("@IdProduct", idProduct);
        return Convert.ToDecimal(await cmd.ExecuteScalarAsync());
    }

    public async Task FulfillOrderAsync(int idOrder, DateTime date)
    {
        await using var command = CreateSqlCommand("UPDATE [Order] SET FulfilledAt = @Date WHERE IdOrder = @IdOrder");
        command.Parameters.AddWithValue("@IdOrder", idOrder);
        command.Parameters.AddWithValue("@Date", date);
        
        await command.ExecuteNonQueryAsync();
    }

    public async Task<int> AddProductToWarehouseAsync(WarehouseProduct data)
    {
        await using var command = CreateSqlCommand("INSERT INTO Product_Warehouse VALUES(@IdProduct, @IdWarehouse, @IdOrder, @Amount, @Price, @CreatedAt);" +
                                       "SELECT SCOPE_IDENTITY()");
        command.Parameters.AddWithValue("@IdProduct", data.ProductId);
        command.Parameters.AddWithValue("@IdWarehouse", data.WarehouseId);
        command.Parameters.AddWithValue("@Amount", data.Amount);
        command.Parameters.AddWithValue("@Price", data.Price);
        command.Parameters.AddWithValue("@IdOrder", data.OrderId);
        command.Parameters.AddWithValue("@CreatedAt", data.CreatedAt);

        var pk = Convert.ToInt32(await command.ExecuteScalarAsync());
        return pk;
    }

    public void BeginTransaction()
    {
        _transaction = _connection.BeginTransaction();
    }

    public async void CommitTransaction()
    {
        if (_transaction!=null)
            await _transaction.CommitAsync();
    }

    private SqlCommand CreateSqlCommand(string sql)
    {
        var cmd = _connection.CreateCommand();
        if (_transaction != null)
            cmd.Transaction = _transaction;
        cmd.CommandText = sql;
        return cmd;
    }
}