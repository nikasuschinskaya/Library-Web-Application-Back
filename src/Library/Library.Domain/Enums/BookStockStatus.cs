using Library.Domain.Extentions;

namespace Library.Domain.Enums;

public enum BookStockStatus
{
    [StringValue("В наличии")]
    InStock,
    [StringValue("Нет в наличии")]
    NotInStock
}