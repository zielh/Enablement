namespace SampleRestAPI2.BLL.DTO
{
    public class OrdersDTO
    {
        public Guid? Id { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
    }
    public class OrdersWithUsersDTO : OrdersDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
