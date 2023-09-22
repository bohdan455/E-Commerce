using Application.Dto;
using DataAccess.Entities;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductBriefInformation>> GetAll();
        Task<ProductFullInformation> GetById(int id);
        Task<List<ProductBriefInformation>> GetMultiple(List<int> productsId);
    }
}