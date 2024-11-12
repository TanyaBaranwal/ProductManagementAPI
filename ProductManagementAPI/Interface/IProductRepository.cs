using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagementAPI.Models.Entities;
namespace ProductManagementAPI.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<Product> DecrementStockAsync(int id, int quantity);
        Task<Product> AddToStockAsync(int id, int quantity);
    }
}