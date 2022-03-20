namespace SampleRestAPI2.BLL.DTO
{
    public class CountriesDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
    public class CountriesWithMerchantDTO : CountriesDTO
    {
        public List<MerchantsDTO> Merchants { get; set; }
    }
}
