using System.Collections.Generic;

namespace RestAppDAL.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }

        public List<CustomerAddress> Customers { get; set; }
    }
}