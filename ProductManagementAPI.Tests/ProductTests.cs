using Moq;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Controllers;
using ProductManagementAPI.Interface;
using ProductManagementAPI.Models.Dto;
using ProductManagementAPI.Models.Entities;

namespace ProductManagementAPI.Tests
{
    [TestFixture]
    public class ProductTests
    {
        private ProductController _controller;
        private Mock<IProductService> _mockProductService;

        [SetUp]
        public void SetUp()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController(_mockProductService.Object);
        }

        // Tests for GetProductById
        [Test]
        public async Task GetProductById_ProductExists_ReturnsOkResult()
        {
            var productId = 1;
            var product = new Product { Id = productId, Name = "Test Product", Stock = 100 };
            _mockProductService.Setup(service => service.GetProductById(productId)).ReturnsAsync(product);

            var result = await _controller.GetProductById(productId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task UpdateProduct_UpdateFails_ReturnsInternalServerError()
        {
            var productId = 1;
            var productDto = new ProductDto { Name = "Updated Product", Description = "Updated Description", Price = 100, Stock = 10 };

            _mockProductService.Setup(service => service.UpdateProduct(productId, productDto)).ReturnsAsync(false);

            var result = await _controller.UpdateProduct(productId, productDto) as ObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(500));
                Assert.That(result.Value, Is.EqualTo("Failed to update product"));
            });
        }

        // Tests for DeleteProduct
        [Test]
        public async Task DeleteProduct_ValidProduct_ReturnsNoContent()
        {
            var productId = 1;

            _mockProductService.Setup(service => service.RemoveProduct(productId)).ReturnsAsync(true);

            var result = await _controller.DeleteProduct(productId) as NoContentResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public async Task DeleteProduct_ProductNotFound_ReturnsInternalServerError()
        {
            var productId = 1;

            _mockProductService.Setup(service => service.RemoveProduct(productId)).ReturnsAsync(false);

            var result = await _controller.DeleteProduct(productId) as ObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(500));
                Assert.That(result.Value, Is.EqualTo("Failed to update product"));
            });
        }
    }
}