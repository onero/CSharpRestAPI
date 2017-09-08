using System.Collections.Generic;

namespace RestAppDAL.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public IList<Order> Orders { get; set; }
    }
}