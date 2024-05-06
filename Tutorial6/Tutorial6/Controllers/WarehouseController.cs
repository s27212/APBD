using Microsoft.AspNetCore.Mvc;
using Tutorial6.Database;
using Tutorial6.DTO;
using Tutorial6.Entities;
using Tutorial6.Services;

namespace Tutorial6.Controllers;

[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;
    private readonly IDatabase _db;

    public WarehouseController(IWarehouseService warehouseService, IDatabase database)
    {
        _warehouseService = warehouseService;
        _db = database;
    }

    [Route("api/addProduct")]
    [HttpPost]
    public async Task<IActionResult> AddProductAsync(AddProductDTO productDto)
    {
        try
        {
            var productInfo = MapNewProductInfo(productDto);
            var whProduct = await _warehouseService.AddProductAsync(productInfo);
            return Ok(whProduct.Id);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/api/addProductStoredProc")]
    public async Task<IActionResult> AddProductProcedureAsync(AddProductDTO productDto)
    {
        try
        {
            var productInfo = MapNewProductInfo(productDto);
            return Ok(await _db.AddProductProcedureAsync(productInfo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private static NewProductInfo MapNewProductInfo(AddProductDTO productDto)
    {
        var productInfo = new NewProductInfo
        {
            ProductId = productDto.IdProduct!.Value,
            WarehouseId = productDto.IdWarehouse!.Value,
            Amount = productDto.Amount!.Value,
            CreatedAt = productDto.CreatedAt!.Value
        };
        return productInfo;
    }
}