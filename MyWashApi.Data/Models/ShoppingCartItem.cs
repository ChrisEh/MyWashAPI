using System;

namespace MyWashApi.Data.Models
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
