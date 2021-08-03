using System;

namespace MyWashApi.Data.Models
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }    
    }
}
