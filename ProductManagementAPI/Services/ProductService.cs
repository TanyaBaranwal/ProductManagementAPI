using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Interface;
using ProductManagementAPI.Models.Dto;
using ProductManagementAPI.Models.Entities;

namespace ProductManagementAPI.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductIdGenerator _productIdGenerator;
        public ProductService(IProductRepository productRepository, ProductIdGenerator productIdGenerator)
        {
            _productRepository= productRepository;
            _productIdGenerator= productIdGenerator;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<Product> AddProduct(string name, string description, decimal price, int stock)
        {
            var product = new Product
            {
                Id = _productIdGenerator.GenerateUniqueProductId(name),
                Name = name,
                Description = description,
                Price = price,
                Stock = stock
            };
            if(product == null){
                throw new ArgumentNullException(nameof(product),"Product cannot be null");
            }
            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<bool> UpdateProduct(int id, ProductDto product)
        {
            var existingProduct= await _productRepository.GetProductByIdAsync(id);
            if(existingProduct==null){
                return false;
            }
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
            
            await _productRepository.UpdateProductAsync(existingProduct);
            return true;
        }

        public async Task<bool> RemoveProduct(int id)
        {
            var existingProduct= await _productRepository.GetProductByIdAsync(id);
            if(existingProduct==null){
                return false;
            }
            return await _productRepository.DeleteProductAsync(id);
            
        }

        public async Task<Product> DecrementStock(int id, int quantity)
        {
            return await _productRepository.DecrementStockAsync(id, quantity);
        }
        public async Task<Product> AddToStock(int id, int quantity)
        {
            return await _productRepository.AddToStockAsync(id,quantity); 
        }


    }
}