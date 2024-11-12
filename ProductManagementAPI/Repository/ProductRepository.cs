using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Interface;
using ProductManagementAPI.Models.Context;
using ProductManagementAPI.Models.Entities;
namespace ProductManagementAPI.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ProductDbContext _productDbContext;
        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext= productDbContext;
        }


        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productDbContext.Products.FindAsync(id);
        }
        public async Task<Product> CreateProductAsync(Product product)
        {

            _productDbContext.Products.Add(product);
            await _productDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var existingProduct= await _productDbContext.Products.FindAsync(product.Id);
            if (existingProduct == null){
                return null;
            }
            _productDbContext.Entry(existingProduct).CurrentValues.SetValues(product);
            await _productDbContext.SaveChangesAsync();
            return existingProduct;
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var productToDelete= await _productDbContext.Products.FindAsync(id);
            if(productToDelete==null){
                return false;
            }

                _productDbContext.Products.Remove(productToDelete);
                await _productDbContext.SaveChangesAsync();
                return true;
        }
        public async Task<Product> DecrementStockAsync(int id, int quantity)
        {
            var product = await _productDbContext.Products.FindAsync(id);
            if (product == null) return null;

            product.Stock -= quantity;
            await _productDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> AddToStockAsync(int id, int quantity)
        {
            var product = await _productDbContext.Products.FindAsync(id);
            if (product == null) return null;

            product.Stock += quantity;
            await _productDbContext.SaveChangesAsync();
            return product;
        }
    }
}