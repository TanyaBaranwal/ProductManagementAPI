using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManagementAPI.Models.Entities;
using ProductManagementAPI.Models.Dto;

namespace ProductManagementAPI.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(string name, string description, decimal price, int stock);
        Task<bool> UpdateProduct(int id, ProductDto product);
        Task<bool> RemoveProduct(int id);
        Task<Product> DecrementStock(int id, int quantity);
        Task<Product> AddToStock(int id, int quantity);
    }
}