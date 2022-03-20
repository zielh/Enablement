namespace SampleRestAPI2Auth.DTO
{
    public class UsersDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid CountryId { get; set; }
        public List<Guid> RolesId { get; set; }
    }
}
