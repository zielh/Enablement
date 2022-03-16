namespace SampleRestAPI2.DTO
{
    public class MerchantsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid CountryId { get; set; }
    }
    public class MerchantsWithUserAndCountryDTO : MerchantsDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CountryName { get; set; }
    }
}
