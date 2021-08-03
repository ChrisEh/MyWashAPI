using System;

namespace MyWashApi.Dtos
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }
        public string Place { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string Password { get; set; }
    }
}
