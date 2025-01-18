using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
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

    public async Task<List<Product>> Search(string searchPhrase)
    {
        var searchDescriptor = new SearchRequestDescriptor<Product>();
        searchDescriptor.Query(q => q.Bool(b => b
                                       .Should(
                                           s => s.Wildcard(w => w
                                               .Field(f => f.Title)
                                               .Value($"*{searchPhrase}*")
                                               .Boost(2)),
                                           s => s.Wildcard(w => w
                                               .Field(f => f.Description)
                                               .Value($"*{searchPhrase}*")
                                               .Boost(1))
                                       )
                                   ))
                                   .Size(10);
        var searchResponse = await _productRepository.Query(searchDescriptor);
        
        
        var hits = searchResponse?.Hits;
        foreach (var hit in hits ?? Enumerable.Empty<Hit<Product>>())
        {
            hit.Source.Id = hit.Id;
        }

        return hits?.Select(x => x.Source).ToList();
    }
}