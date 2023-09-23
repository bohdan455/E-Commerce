using Presentation.ApiRequests.Interfaces;
using Presentation.Models;

namespace Presentation.ApiRequests
{
    public class ProductApiRequests : IProductApiRequests
    {
        private readonly HttpClient _httpClient;

        public ProductApiRequests(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<ProductBriefInformation>> GetAll()
        {
            var products = await _httpClient.GetFromJsonAsync<List<ProductBriefInformation>>("api/product");

            return products ?? throw new ArgumentException("No products found");
        }

        public async Task<ProductFullInformation> GetFullInformation(int id)
        {
            var product = await _httpClient.GetFromJsonAsync<ProductFullInformation>($"api/product/{id}");

            return product ?? throw new ArgumentException("No product found");
        }
    }
}
