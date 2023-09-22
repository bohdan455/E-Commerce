using Application.Controllers;
using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _productController = new ProductController(_mockProductService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithListOfProducts()
        {
            // Arrange
            var products = new List<ProductBriefInformation>
        {
            new ProductBriefInformation {Id = 1, Name = "Product 1", Price = 10.00M},
            new ProductBriefInformation {Id = 2, Name = "Product 2", Price = 15.00M}
        };
            _mockProductService.Setup(x => x.GetAll()).ReturnsAsync(products);

            // Act
            var result = await _productController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductBriefInformation>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetAll_ShouldReturnNotFoundWhenArgumentExceptionIsThrown()
        {
            // Arrange
            _mockProductService.Setup(x => x.GetAll()).ThrowsAsync(new ArgumentException("Invalid product"));

            // Act
            var result = await _productController.GetAll();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkWithProductFullInformation()
        {
            // Arrange
            var product = new ProductFullInformation { Id = 1, Name = "Product 1", Price = 10.00M };
            _mockProductService.Setup(x => x.GetById(1)).ReturnsAsync(product);

            // Act
            var result = await _productController.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ProductFullInformation>(okResult.Value);
            Assert.Equal("Product 1", model.Name);
            Assert.Equal(10.00M, model.Price);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFoundWhenArgumentExceptionIsThrown()
        {
            // Arrange
            _mockProductService.Setup(x => x.GetById(1)).ThrowsAsync(new ArgumentException("Invalid product"));

            // Act
            var result = await _productController.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
