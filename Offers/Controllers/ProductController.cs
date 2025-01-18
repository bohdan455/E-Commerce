using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Offers.Common.Models;
using Offers.Requests;
using Offers.Services.Services.Interfaces;

namespace Offers.Controllers;

[Route("api/Product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAll();
        
        return Ok(products);
    }
    
    [HttpGet("{key}")]
    public async Task<IActionResult> GetById([FromRoute] string key)
    {
        var products = await _productService.Get(key);

        return products is null ? NotFound() : Ok(products);
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery][Required] string searchPhrase)
    {
        var products = await _productService.Search(searchPhrase);

        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdate([FromBody]ProductRequest product)
    {
        var newProduct = await _productService.AddOrUpdate(product.ToModel());

        return Ok(newProduct);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove([FromBody] string key)
    {
        var isDeleted = await _productService.Remove(key);

        return isDeleted ? NoContent() : NotFound();
    }
}