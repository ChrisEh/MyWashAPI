namespace MyWashApi.Dtos
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Place { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
    }
}
