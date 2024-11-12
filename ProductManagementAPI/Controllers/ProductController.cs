using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Interface;
using ProductManagementAPI.Models.Entities;
using ProductManagementAPI.Models.Dto;
using ProductManagementAPI.Services;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        
        public ProductController(IProductService productService)
        {
            _productService=productService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(){
            try{
                var products = await _productService.GetAllProducts();
                if(products==null || !products.Any()){
                    return NotFound("No products available.");
                }
                return Ok(products);
            }
            catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id){
            try{
                var product = await _productService.GetProductById(id);
                if(product==null){
                    return NotFound("No products found.");
                }
                return Ok(product);
            }
            catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductDto productDto)
    {
        try{
            if (productDto == null)
                {
                    return BadRequest("Invalid product data");
                }
        var addResult = await _productService.AddProduct(productDto.Name, productDto.Description, productDto.Price, productDto.Stock);
        if (addResult == null)
                {
                    return StatusCode(500, "Failed to add product");
                }
        return CreatedAtAction(nameof(GetProductById), new { id = addResult.Id }, addResult);
    
        }
        catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
    {
        try{
            if (productDto == null || id<=0)
                {
                    return BadRequest("Product cannot be null or have an invalid ID");
                }

        var updateResult=await _productService.UpdateProduct(id,productDto);
        if (updateResult)
                {
                    return NoContent();  // 204 No Content on success
                }

                return StatusCode(500, "Failed to update product");  // Error case when update fails
            
        }
        catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try{
            if (id <=0)
            {
                return BadRequest("Product Id is invalid");
            }
            var deleteResult=await _productService.RemoveProduct(id);
            if(deleteResult){
                return NoContent();
            }
        return StatusCode(500, "Failed to update product");  // Error case when update fails
        }
        catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            } 
    }
    

    [HttpPut("decrement-stock/{id}/{quantity}")]
    public async Task<IActionResult> DecrementStock(int id, int quantity)
    {
        try{
            if (id <= 0)
        {
            return BadRequest("Product Id is invalid");
        }
        if (quantity <=0)
        {
            return BadRequest("Product quantity is invalid");
        }
        var result= await _productService.GetProductById(id);
        if(result!=null){
            if(result.Stock <quantity){
                return BadRequest("Insufficient stock to decrement.");
            }
        }
        else{
            return NotFound();
        }
        var product = await _productService.DecrementStock(id, quantity);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(new { product.Id, product.Name, product.Stock });
        }
        catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
    }

    [HttpPut("add-to-stock/{id}/{quantity}")]
    public async Task<IActionResult> AddToStock(int id, int quantity)
    {
        try{
            if (id <= 0)
        {
            return BadRequest("Product Id is invalid");
        }
        if (quantity <=0)
        {
            return BadRequest("Product quantity is invalid");
        }
       
        var product = await _productService.AddToStock(id, quantity);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(new { product.Id, product.Name, product.Stock });
        }
        catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
    }
    }
}