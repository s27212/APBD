using Tutorial6.Models;

namespace Tutorial6.Repositories;

public interface IProductService
{
    public Task<decimal> AddProductAsync(WarehouseProductDTO productDto);
    public Task<decimal> AddProductProcedureAsync(WarehouseProductDTO productDto);
}