using Application.Dto;
using Application.Extensions;
using Application.Services.Interfaces;
using DataAccess;
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

        public async Task<ProductFullInfromation> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return product?.ToFullInfromation() ?? throw new ArgumentException("Invalid product id");
        }

    }
}
