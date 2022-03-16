namespace SampleRestAPI2.DTO
{
    public class UsersDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid CountryId { get; set; }
    }
    public class UsersWithCountryDTO : UsersDTO
    {
        public string CountryName { get; set; }
    }
}
