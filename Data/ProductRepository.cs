// Data/ProductRepository.cs
using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyOrderService.Data
{
    public class ProductRepository
    {
        private readonly Dictionary<string, double> _productPrices = new()
        {
            ["Widget"] = 12.99,
            ["Gadget"] = 15.49,
            ["Doohickey"] = 8.75
        };

        public bool TryGetPrice(string productName, out double price)
        {
            // Simulate an expensive lookup
            Thread.Sleep(500);
            return _productPrices.TryGetValue(productName, out price);
        }
    }
}