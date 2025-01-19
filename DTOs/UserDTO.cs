namespace FormFiller.DTO
{
    public class UserDTO
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string HouseNumberAddition { get; set; }
    }
}
