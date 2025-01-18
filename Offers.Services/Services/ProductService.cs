using Offers.Common.Models;
using Offers.Services.Data.Repositories.Interfaces;
using Offers.Services.Services.Interfaces;

namespace Offers.Services.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Get(string key)
    {
        var product = await _productRepository.Get(key);

        return product.Source;
    }

    public async Task<Product> AddOrUpdate(Product product)
    {
        var newProduct = await _productRepository.AddOrUpdate(product);
        
        return newProduct;
    }
    public async Task<List<Product>> GetAll()
    {
        var products = await _productRepository.GetAll();
        var hits = products.Hits;
        foreach (var hit in hits)
        {
            hit.Source.Id = hit.Id;
        }

        return hits.Select(x => x.Source).ToList();
    }

    public async Task<bool> Remove(string key)
    {
        var isDeleted = await _productRepository.Remove(key);

        return isDeleted;
    }
}