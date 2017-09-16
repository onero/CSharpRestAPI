using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestAppBLL.BusinessObjects
{
    public class CustomerBO
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        public List<int> AddressIds { get; set; }

        public List<AddressBO> Addresses { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}