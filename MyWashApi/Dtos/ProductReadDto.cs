using System;

namespace MyWashApi.Dtos
{
    public class ProductReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsPopularProduct { get; set; }
        public int CategoryId { get; set; }
    }
}
