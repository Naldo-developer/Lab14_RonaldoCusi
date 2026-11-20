using Lab08RonaldoCusi.Data;
using Lab08RonaldoCusi.DTOs;
using Lab08RonaldoCusi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Lab08RonaldoCusi.Repositories;
public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public Task<List<OrderDto>> GetOrdersAfterDateExercise6Async(DateTime fecha, CancellationToken ct = default) =>
        context.Orders.AsNoTracking().Where(o => o.Orderdate > DateTime.SpecifyKind(fecha, DateTimeKind.Unspecified)).Select(o => new OrderDto(o.Orderid,o.Clientid,o.Orderdate)).ToListAsync(ct);

    public Task<ClientOrderCountDto?> GetClientWithMostOrdersExercise9Async(CancellationToken ct = default) =>
        context.Orders.AsNoTracking().GroupBy(o => o.Clientid)
            .Select(g => new { ClientId = g.Key, OrderCount = g.Count() })
            .OrderByDescending(c => c.OrderCount).ThenBy(c => c.ClientId)
            .Select(c => new ClientOrderCountDto(c.ClientId,
                context.Clients.Where(client => client.Clientid == c.ClientId).Select(client => client.Name).First(),
                c.OrderCount))
            .FirstOrDefaultAsync(ct);

    public Task<List<ProductNameDto>> GetProductsSoldToClientExercise11Async(int clientId, CancellationToken ct = default) =>
        context.Orders.AsNoTracking().Where(o => o.Clientid == clientId).SelectMany(o => o.Orderdetails).Select(d => d.Product.Name).Distinct().Select(n => new ProductNameDto(n)).ToListAsync(ct);

    public Task<List<ClientDto>> GetClientsWhoBoughtProductExercise12Async(int productId, CancellationToken ct = default) =>
        context.Orderdetails.AsNoTracking().Where(d => d.Productid == productId).Select(d => new {d.Order.Client.Clientid,d.Order.Client.Name,d.Order.Client.Email}).Distinct().Select(c => new ClientDto(c.Clientid,c.Name,c.Email)).ToListAsync(ct);
    // LAB 09 - Paso 3: Include y ThenInclude
    public Task<List<OrderDetailsDto>> GetOrdersWithProductsAsync(
        CancellationToken ct = default) =>
        context.Orders
            .Include(order => order.Orderdetails)
            .ThenInclude(detail => detail.Product)
            .AsNoTracking()
            .Select(order => new OrderDetailsDto(
                order.Orderid,
                order.Orderdate,
                order.Orderdetails.Select(detail => new OrderProductDto(
                    detail.Product.Name,
                    detail.Quantity,
                    detail.Product.Price
                )).ToList()
            ))
            .ToListAsync(ct);
    // LAB 09 - Paso 5: Agrupación de ventas por cliente
    public Task<List<SalesByClientDto>> GetSalesByClientAsync(
        CancellationToken ct = default) =>
        context.Orderdetails
            .AsNoTracking()
            .GroupBy(d => new
            {
                d.Order.Clientid,
                d.Order.Client.Name
            })
            .Select(g => new SalesByClientDto(
                g.Key.Name,
                g.Sum(d => d.Quantity * d.Product.Price)
            ))
            .OrderByDescending(x => x.TotalSales)
            .ToListAsync(ct);
}
