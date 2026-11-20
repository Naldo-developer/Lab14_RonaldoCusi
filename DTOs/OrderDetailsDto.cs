namespace Lab08RonaldoCusi.DTOs;

public record OrderDetailsDto(
    int OrderId,
    DateTime OrderDate,
    List<OrderProductDto> Products
);