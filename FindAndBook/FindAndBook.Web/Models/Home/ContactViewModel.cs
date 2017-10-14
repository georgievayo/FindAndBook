using System.ComponentModel.DataAnnotations;

namespace FindAndBook.Web.Models.Home
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(3)]
        [RegularExpression("[a-zA-Z]{3,}")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [RegularExpression("[a-zA-Z]{3,}")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required]
        [MinLength(10)]
        public string Question { get; set; }
    }
}