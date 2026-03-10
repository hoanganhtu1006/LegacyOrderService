using LegacyOrderService.Data;
using LegacyOrderService.Models;

namespace LegacyOrderService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Order Processor!");
            Console.WriteLine("Enter customer name:");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Customer name cannot be empty.");
                return;
            }

            Console.WriteLine("Enter product name:");
            string product = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(product))
            {
                Console.WriteLine("Product name cannot be empty.");
                return;
            }

            var productRepo = new ProductRepository();
            var result = await productRepo.TryGetPriceAsync(product);

            if (!result.found)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            double price = result.price;

            Console.WriteLine("Enter quantity:");

            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Quantity must be a positive number.");
                return;
            }

            Console.WriteLine("Processing order...");

            Order order = new Order();
            order.CustomerName = name;
            order.ProductName = product;
            order.Quantity = qty;
            order.Price = price;

            double total = order.Quantity * order.Price;

            Console.WriteLine("Order complete!");
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Total: $" + total);

            Console.WriteLine("Saving order to database...");
            var repo = new OrderRepository();
            repo.Save(order);
            Console.WriteLine("Done.");
        }
    }
}