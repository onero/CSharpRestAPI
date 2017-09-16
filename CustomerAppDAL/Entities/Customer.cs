using System.Collections.Generic;

namespace RestAppDAL.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Order> Orders { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}