using LegacyOrderService.Interfaces;
using LegacyOrderService.Models;

namespace LegacyOrderService.Services;

public class OrderService
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderService(IProductRepository productRepository, IOrderRepository orderRepository)
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