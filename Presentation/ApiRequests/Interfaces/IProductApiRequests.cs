using Presentation.Models;

namespace Presentation.ApiRequests.Interfaces
{
    public interface IProductApiRequests
    {
        Task<List<ProductBriefInformation>> GetAll();
        Task<ProductFullInformation> GetFullInformation(int id);
    }
}