using Lab08RonaldoCusi.DTOs;
namespace Lab08RonaldoCusi.Repositories.Interfaces;
public interface IOrderRepository
{
    Task<List<OrderDto>> GetOrdersAfterDateExercise6Async(DateTime fecha, CancellationToken ct = default);
    Task<ClientOrderCountDto?> GetClientWithMostOrdersExercise9Async(CancellationToken ct = default);
    Task<List<ProductNameDto>> GetProductsSoldToClientExercise11Async(int clientId, CancellationToken ct = default);
    Task<List<ClientDto>> GetClientsWhoBoughtProductExercise12Async(int productId, CancellationToken ct = default);
    
    // LAB 09 - Paso 3
    Task<List<OrderDetailsDto>> GetOrdersWithProductsAsync(
        CancellationToken ct = default);
    // LAB 09 - Paso 5
    Task<List<SalesByClientDto>> GetSalesByClientAsync(
        CancellationToken ct = default);
}
