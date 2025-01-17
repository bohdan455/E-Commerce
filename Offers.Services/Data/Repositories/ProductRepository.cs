using Microsoft.Extensions.Options;
using Offers.Common.Models;
using Offers.Options;
using Offers.Services.Data.Repositories.Base;
using Offers.Services.Data.Repositories.Interfaces;

namespace Offers.Services.Data.Repositories;

public class ProductRepository : BaseElasticRepository<Product>, IProductRepository
{
    public ProductRepository(IOptions<ElasticSearchOptions> options) : base(options)
    {
    }
}