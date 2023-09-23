using Microsoft.AspNetCore.Mvc;
using Presentation.ApiRequests.Interfaces;

namespace Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiRequests _productApi;

        public ProductController(IProductApiRequests productApi)
        {
            _productApi = productApi;
        }

        public async Task<IActionResult> Products()
        {
            return View(await _productApi.GetAll());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _productApi.GetFullInformation(id));
        }
    }
}
