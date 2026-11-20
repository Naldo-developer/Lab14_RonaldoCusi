namespace Lab08RonaldoCusi.DTOs;

public record ClientOrderDto(
    string ClientName,
    List<OrderDto> Orders
);