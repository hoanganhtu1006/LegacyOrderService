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

            string name = ReadRequiredString("Enter customer name:");
            string product = ReadRequiredString("Enter product name:");
            int qty = ReadQuantity();

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

            Console.WriteLine("Order complete!");
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Total: $" + order.Total);

            Console.WriteLine("Saving order to database...");
            orderService.SaveOrder(order);

            Console.WriteLine("Done.");
        }

        static string ReadRequiredString(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("Value cannot be empty.");
            }
        }
        static int ReadQuantity()
        {
            while (true)
            {
                Console.WriteLine("Enter quantity:");

                if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
                    return qty;

                Console.WriteLine("Quantity must be a positive number.");
            }
        }
    }
}