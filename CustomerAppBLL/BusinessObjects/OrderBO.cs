using System;

namespace RestAppBLL.BusinessObjects
{
    public class OrderBO
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}