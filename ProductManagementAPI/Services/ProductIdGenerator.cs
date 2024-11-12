using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Services
{
    public class ProductIdGenerator
    {
        private static readonly Random _random = new Random();
        public int GenerateUniqueProductId(string productName)
        {
            var timestamp = DateTime.UtcNow.Ticks; // Get the current timestamp
            var randomValue = _random.Next(1000, 9999); // Random value for added uniqueness

            // Combine timestamp, product name, and random value into a single string
            var hash = $"{timestamp}{productName}{randomValue}";
            
            // Use GetHashCode() to generate a hash and then ensure it fits within a 6-digit integer
            var productId = Math.Abs(hash.GetHashCode()) % 1000000; // Ensure productId is within a 6-digit range

            // Return the productId as an integer
            return productId;
        }
    }
}