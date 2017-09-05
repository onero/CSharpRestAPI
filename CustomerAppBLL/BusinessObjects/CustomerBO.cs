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
        [Required]
        public string Address { get; set; }

        public string FullName() => $"{FirstName} {LastName}";
    }
}