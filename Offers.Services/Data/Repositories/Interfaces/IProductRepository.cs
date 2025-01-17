using Offers.Common.Models;
using Offers.Services.Data.Repositories.Base;

namespace Offers.Services.Data.Repositories.Interfaces;

public interface IProductRepository : IBaseElasticRepository<Product>
{
    
}