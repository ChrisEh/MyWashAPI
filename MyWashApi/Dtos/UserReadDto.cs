﻿using System;
using System.Collections.Generic;
using MyWashApi.Data.Models;

namespace MyWashApi.Dtos
{
    public class UserReadDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public ICollection<Pickup> Pickups { get; set; }
        public string Place { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
    }
}