using Microsoft.AspNetCore.Mvc;
using Tutorial6.Models;
using Tutorial6.Repositories;

namespace Tutorial6.Controllers;

[ApiController]
[Route("api/warehouse")]
public class WarehouseController : ControllerBase
{
    private readonly IProductService _productrepository;

    public WarehouseController(IProductService productService)
    {
        _productrepository = productService;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync(WarehouseProductDTO productDto)
    {
        try
        {
            return Ok(await _productrepository.AddProductAsync(productDto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("/api/procedure")]
    public async Task<IActionResult> AddProductProcedureAsync(WarehouseProductDTO productDto)
    {
        try
        {
            return Ok(await _productrepository.AddProductProcedureAsync(productDto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}