using System.Collections.Generic;
using RestAppDAL.Entities;

namespace RestAppBLL.BusinessObjects
{
    public class AddressBO
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
    }
}