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

        [NotMapped]
        //[JsonIgnore]
        public byte[] ImageArray { get; set; }
        
        //[JsonIgnore]
        //public ICollection<PickupDetail> PickupDetails { get; set; }
    }
}
