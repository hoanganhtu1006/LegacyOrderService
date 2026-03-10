using LegacyOrderService.Data;
using LegacyOrderService.Interfaces;
using LegacyOrderService.Services;

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

            Console.WriteLine("Enter quantity:");

            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Quantity must be a positive number.");
                return;
            }

            IProductRepository productRepo = new ProductRepository();
            IOrderRepository orderRepo = new OrderRepository();

            var orderService = new OrderService(productRepo, orderRepo);

            Console.WriteLine("Processing order...");

            var order = await orderService.CreateOrderAsync(name, product, qty);

            if (order == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            double total = order.Quantity * order.Price;

            Console.WriteLine("Order complete!");
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Total: $" + total);

            Console.WriteLine("Saving order to database...");
            orderService.SaveOrder(order);

            Console.WriteLine("Done.");
        }
    }
}