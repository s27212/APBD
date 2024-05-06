using Tutorial6.Entities;

namespace Tutorial6.Services;

public interface IWarehouseService
{
    Task<WarehouseProduct> AddProductAsync(NewProductInfo info);
}