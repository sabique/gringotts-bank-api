using System.ComponentModel.DataAnnotations;

namespace ServiceModel
{
    public class Customer
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
