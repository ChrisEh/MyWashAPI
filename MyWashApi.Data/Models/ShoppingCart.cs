﻿using System;
using System.Collections.Generic;

namespace MyWashApi.Data.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}