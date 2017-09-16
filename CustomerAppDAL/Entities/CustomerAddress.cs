namespace RestAppDAL.Entities
{
    public class CustomerAddress
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}