using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetWebApiCrud.Data;
using DotnetWebApiCrud.Models;

namespace DotnetWebApiCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetLists()
    {
        var products = await _db.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
        {
            return NotFound(new
            {
                statusCode = 404,
                message = "Product not found",
                result = (object?)null
            });
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ModifyProduct product)
    {
        if (product == null)
        {
            return BadRequest(new
            {
                statusCode = 400,
                message = "Product data is required",
                result = (object?)null
            });
        }

        if (product.Price <= 0)
        {
            return BadRequest(new
            {
                statusCode = 400,
                message = "Price of product must be greater than 0",
                result = (object?)null
            });
        }

        var newProduct = new Product
        {
            Name = product.Name,
            Price = product.Price
        };

        _db.Products.Add(newProduct);
        await _db.SaveChangesAsync();
        return Ok(new
        {
            statusCode = 201,
            message = "Product Created successfully",
            result = new { 
                id = newProduct.Id,
                Name = newProduct.Name,
                Price = newProduct.Price
            }
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ModifyProduct product)
    {
        var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (p == null)
        {
            return NotFound(new
            {
                statusCode = 404,
                message = "Product not found",
                result = (object?)null
            });
        }
        p.Name = product.Name;
        p.Price = product.Price;
        await _db.SaveChangesAsync();
        return Ok(new
        {
            statusCode = 200,
            message = "Product updated successfully",
            result = p
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (p == null)
        {
            return NotFound(new
            {
                statusCode = 404,
                message = "Product not found",
                result = (object?)null
            });
        }
        _db.Products.Remove(p);
        await _db.SaveChangesAsync();
        return Ok(new
        {
            statusCode = 200,
            message = "Product deleted successfully",
            result = true
        });
    }
}
