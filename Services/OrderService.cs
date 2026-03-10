using LegacyOrderService.Data;
using LegacyOrderService.Models;

namespace LegacyOrderService.Services;

public class OrderService
{
    private readonly ProductRepository _productRepository;
    private readonly OrderRepository _orderRepository;

    public OrderService(ProductRepository productRepository, OrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Order?> CreateOrderAsync(string customerName, string productName, int quantity)
    {
        var result = await _productRepository.TryGetPriceAsync(productName);

        if (!result.found)
            return null;

        return new Order
        {
            CustomerName = customerName,
            ProductName = productName,
            Quantity = quantity,
            Price = result.price
        };
    }

    public void SaveOrder(Order order)
    {
        _orderRepository.Save(order);
    }
}