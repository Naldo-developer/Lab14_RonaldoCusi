namespace Lab08RonaldoCusi.DTOs;

public record OrderProductDto(
    string ProductName,
    int Quantity,
    decimal Price
);