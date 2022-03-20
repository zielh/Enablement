namespace SampleRestAPI2.BLL.DTO
{
    public class OrdersItemsDTO
    {
        public Guid? Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class OrdersItemsWithProductAndOrderDTO : OrdersItemsDTO
    {
        public string ProductName { get; set; }
        public string Status { get; set; }
    }
}
