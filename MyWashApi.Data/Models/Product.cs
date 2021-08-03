using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWashApi.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }        
        public double Price { get; set; }
        public bool IsPopularProduct { get; set; }  
        public int CategoryId { get; set; }

        [NotMapped]
        //[JsonIgnore]
        public byte[] ImageArray { get; set; }
        
        //[JsonIgnore]
        //public ICollection<OrderDetail> OrderDetails { get; set; }
        
        //[JsonIgnore]
        //public ICollection<ShoppingCart> ShoppingCartItems { get; set; }
    }
}
