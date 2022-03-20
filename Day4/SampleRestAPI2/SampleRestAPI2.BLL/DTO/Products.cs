namespace SampleRestAPI2.BLL.DTO
{
    public class ProductsDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public Guid MerchantId { get; set; }
        public bool Status { get; set; }
    }
    public class ProductsWithMerchantsDTO : ProductsDTO
    {
        public string MerchantName { get; set; }
    }
}
