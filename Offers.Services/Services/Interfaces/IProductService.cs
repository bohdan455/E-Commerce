using Offers.Common.Models;

namespace Offers.Services.Services.Interfaces;

public interface IProductService
{
    Task<Product> AddOrUpdate(Product product);
    
    Task<List<Product>> GetAll();
}