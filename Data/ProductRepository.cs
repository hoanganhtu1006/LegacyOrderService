// Data/ProductRepository.cs
using LegacyOrderService.Interfaces;

namespace LegacyOrderService.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly Dictionary<string, double> _productPrices = new()
        {
            ["Widget"] = 12.99,
            ["Gadget"] = 15.49,
            ["Doohickey"] = 8.75
        };

        public async Task<(bool found, double price)> TryGetPriceAsync(string productName)
        {
            // Simulate an expensive lookup
            await Task.Delay(500);

            var found = _productPrices.TryGetValue(productName, out var price);

            return (found, price);
        }
    }
}