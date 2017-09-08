using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RestAppBLL.BusinessObjects
{
    public class OrderBO
    {
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }

        public CustomerBO Customer { get; set; }
    }
}