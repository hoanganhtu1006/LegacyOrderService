namespace LegacyOrderService.Interfaces
{
    public interface IProductRepository
    {
        Task<(bool found, double price)> TryGetPriceAsync(string productName);
    }
}
