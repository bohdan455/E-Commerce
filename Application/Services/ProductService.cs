using Application.Dto;
using Application.Extensions;
using Application.Services.Interfaces;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductBriefInformation>> GetAll()
        {
            return await _context.Products.Select(p => p.ToBriefInforamtion()).ToListAsync();
        }

        public async Task<ProductFullInformation> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return product?.ToFullInfromation() ?? throw new ArgumentException("Invalid product id");
        }

        public async Task<List<ProductBriefInformation>> GetMultiple(List<int> productsId)
        {
            var products = await _context.Products
                .Where(p => productsId.Contains(p.Id))
                .Select(p => p.ToBriefInforamtion())
                .ToListAsync();

            if(products.Count != productsId.Count)
            {
                throw new ArgumentException("Invalid product indexes");
            }

            return products;
        }
    }
}
