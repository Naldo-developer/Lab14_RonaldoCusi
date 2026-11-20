using Lab08RonaldoCusi.Data;
using Lab08RonaldoCusi.DTOs;
using Lab08RonaldoCusi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Lab08RonaldoCusi.Repositories;
public class ClientRepository(AppDbContext context) : IClientRepository
{
    public Task<List<ClientDto>> GetClientsByNameExercise1Async(string nombre, CancellationToken ct = default) =>
        context.Clients.AsNoTracking().Where(c => c.Name.Contains(nombre)).Select(c => new ClientDto(c.Clientid,c.Name,c.Email)).ToListAsync(ct);
    //Clientes con sus pedidos
    public Task<List<ClientOrderDto>> GetClientsWithOrdersAsync(
        CancellationToken ct = default) =>
        context.Clients
            .AsNoTracking()
            .Select(client => new ClientOrderDto(
                client.Name,
                client.Orders.Select(order => new OrderDto(
                    order.Orderid,
                    order.Clientid,
                    order.Orderdate
                )).ToList()
            ))
            .ToListAsync(ct);
    // LAB 09 - Paso 4: Doble consulta
    public async Task<List<ClientProductTotalDto>> GetClientProductTotalsAsync(
        CancellationToken ct = default)
    {
        var clientes = await context.Clients
            .AsNoTracking()
            .ToListAsync(ct);

        var cantidades = await context.Orders
            .AsNoTracking()
            .SelectMany(o => o.Orderdetails)
            .GroupBy(d => d.Order.Clientid)
            .Select(g => new
            {
                ClientId = g.Key,
                Total = g.Sum(d => d.Quantity)
            })
            .ToListAsync(ct);

        return clientes
            .Select(c => new ClientProductTotalDto(
                c.Name,
                cantidades
                    .Where(x => x.ClientId == c.Clientid)
                    .Sum(x => x.Total)
            ))
            .ToList();
    }
}
