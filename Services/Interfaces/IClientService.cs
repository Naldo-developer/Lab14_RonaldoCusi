using Lab08RonaldoCusi.DTOs;
namespace Lab08RonaldoCusi.Services.Interfaces;
public interface IClientService
{
    Task<List<ClientDto>> GetClientsByNameExercise1Async(string nombre, CancellationToken ct = default);
    // LAB 09 - Paso 2
    Task<List<ClientOrderDto>> GetClientsWithOrdersAsync(
        CancellationToken ct = default);
    // LAB 09 - Paso 4
    Task<List<ClientProductTotalDto>> GetClientProductTotalsAsync(
        CancellationToken ct = default);
}
