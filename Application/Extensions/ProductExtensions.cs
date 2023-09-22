using Application.Dto;
using DataAccess.Entities;

namespace Application.Extensions
{
    public static class ProductExtensions
    {
        public static ProductBriefInformation ToBriefInforamtion(this Product product)
        {
            return new ProductBriefInformation
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
            };
        }

        public static ProductFullInfromation ToFullInfromation(this Product product)
        {
            return new ProductFullInfromation
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
            };
        }
    }
}
