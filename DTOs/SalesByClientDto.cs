namespace Lab08RonaldoCusi.DTOs;

public record SalesByClientDto(
    string ClientName,
    decimal TotalSales
);