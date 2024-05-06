using Tutorial6.Database;
using Tutorial6.Entities;

namespace Tutorial6.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IDatabase _db;

    public WarehouseService(IDatabase db)
    {
        _db = db;
    }

    public async Task<WarehouseProduct> AddProductAsync(NewProductInfo info)
    {
        _db.BeginTransaction();
        
        if (!await _db.ProductExistsAsync(info.ProductId))
        {
            throw new ArgumentException("Product with provided id does not exist.");
        }

        if (!await _db.WarehouseExistsAsync(info.WarehouseId))
        {
            throw new ArgumentException("Warehouse with provided id does not exist.");
        }

        var orderId = await _db.GetPendingOrderAsync(info.ProductId, info.Amount, info.CreatedAt);
        if (orderId==null)
        {
            throw new ArgumentException("No pending order for given product and amount.");
        }

        await _db.FulfillOrderAsync(orderId.Value, info.CreatedAt);
        
        var price = await _db.GetProductPriceAsync(info.ProductId);

        var whProduct = new WarehouseProduct
        {
            WarehouseId = info.WarehouseId,
            ProductId = info.ProductId,
            OrderId = orderId.Value,
            Amount = info.Amount,
            Price = info.Amount * price,
            CreatedAt = info.CreatedAt
        };

        whProduct.Id = await _db.AddProductToWarehouseAsync(whProduct);

        _db.CommitTransaction();

        return whProduct;
    }
}