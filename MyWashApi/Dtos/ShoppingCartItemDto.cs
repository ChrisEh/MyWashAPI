using System;

namespace MyWashApi.Dtos
{
    public class ShoppingCartItemDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
