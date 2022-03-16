namespace SampleRestAPI2.DTO
{
    public class OrdersItemsDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class OrdersItemsWithProductDTO : OrdersItemsDTO
    {
        public string ProductName { get; set; }
    }
}
