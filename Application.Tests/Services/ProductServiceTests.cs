using Application.Dto;
using Application.Services;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.EntityFrameworkCore;

namespace Application.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<ApplicationDbContext> _mockContext;

        public ProductServiceTests()
        {
            _mockContext = new Mock<ApplicationDbContext>(GetOptions<ApplicationDbContext>());
            _productService = new ProductService(_mockContext.Object);
        }

        private static DbContextOptions<T> GetOptions<T>() where T : DbContext
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [Fact]
        public async Task GetAll_ShouldReturnProductBriefInformation()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.00M },
            new Product { Id = 2, Name = "Product 2", Price = 15.00M }
        };

            _mockContext.Setup(m => m.Products).ReturnsDbSet(products);

            // Act
            var result = await _productService.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<ProductBriefInformation>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnProductFullInfromation()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Price = 10.00M };

            _mockContext.Setup(m => m.Products.FindAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetById(1);

            // Assert
            Assert.IsType<ProductFullInformation>(result);
            Assert.Equal("Product 1", result.Name);
            Assert.Equal(10.00M, result.Price);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldThrowArgumentException()
        {
            // Arrange
            _mockContext.Setup(m => m.Products.FindAsync(1)).ReturnsAsync((Product)null);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _productService.GetById(1));
        }
    }
}
