namespace SampleRestAPI2.BLL.DTO
{
    public class MerchantsDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid CountryId { get; set; }
    }
    public class MerchantsWithUserAndCountryDTO : MerchantsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CountryName { get; set; }
    }
}
