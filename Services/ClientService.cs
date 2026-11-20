using Lab08RonaldoCusi.DTOs;
using Lab08RonaldoCusi.Repositories.Interfaces;
using Lab08RonaldoCusi.Services.Interfaces;
namespace Lab08RonaldoCusi.Services;
public class ClientService(IClientRepository repository) : IClientService
{
    public Task<List<ClientDto>> GetClientsByNameExercise1Async(string nombre, CancellationToken ct = default) =>
        repository.GetClientsByNameExercise1Async(nombre, ct);
    // LAB 09 - Paso 2
    public Task<List<ClientOrderDto>> GetClientsWithOrdersAsync(
        CancellationToken ct = default) =>
        repository.GetClientsWithOrdersAsync(ct);
    // LAB 09 - Paso 4
    public Task<List<ClientProductTotalDto>> GetClientProductTotalsAsync(
        CancellationToken ct = default) =>
        repository.GetClientProductTotalsAsync(ct);
}
