using Lab08RonaldoCusi.DTOs;

namespace Lab08RonaldoCusi.Repositories.Interfaces;

public interface IClientRepository
{
    // LAB 08 - Ejercicio 1
    Task<List<ClientDto>> GetClientsByNameExercise1Async(
        string nombre,
        CancellationToken ct = default);

    // LAB 09 - Paso 2
    Task<List<ClientOrderDto>> GetClientsWithOrdersAsync(
        CancellationToken ct = default);
    
    // LAB 09 - Paso 4
    Task<List<ClientProductTotalDto>> GetClientProductTotalsAsync(
        CancellationToken ct = default);
}