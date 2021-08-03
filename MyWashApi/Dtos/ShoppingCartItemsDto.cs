using System;
using System.Collections.Generic;

namespace MyWashApi.Dtos
{
    public class ShoppingCartItemsDto
    {
        public Guid UserId { get; set; }
        public ICollection<Guid> ProductIds { get; set; }
    }
}
