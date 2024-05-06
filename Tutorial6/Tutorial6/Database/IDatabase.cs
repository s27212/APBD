using Tutorial6.DTO;
using Tutorial6.Entities;

namespace Tutorial6.Database;

public interface IDatabase
{
    public Task<int> AddProductProcedureAsync(NewProductInfo info);
    Task<bool> ProductExistsAsync(int idProduct);
    Task<bool> WarehouseExistsAsync(int idWarehouse);
    Task FulfillOrderAsync(int idOrder, DateTime date);
    void BeginTransaction();
    void CommitTransaction();
    Task<int?> GetPendingOrderAsync(int idProduct, int amount, DateTime createdAt);
    Task<int> AddProductToWarehouseAsync(WarehouseProduct data);
    Task<decimal> GetProductPriceAsync(int idProduct);
}