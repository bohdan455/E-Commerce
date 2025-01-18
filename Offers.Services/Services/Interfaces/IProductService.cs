using Offers.Common.Models;

namespace Offers.Services.Services.Interfaces;

public interface IProductService
{
    Task<Product> Get(string key);
    
    Task<Product> AddOrUpdate(Product product);
    
    Task<List<Product>> GetAll();

    Task<bool> Remove(string key);
    
    Task<List<Product>> Search(string searchPhrase);
}