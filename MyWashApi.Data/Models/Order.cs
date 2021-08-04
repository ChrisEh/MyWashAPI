using System;
using System.Collections.Generic;

namespace MyWashApi.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool IsOrderCompleted { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
