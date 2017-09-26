using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FindAndBook.Web.Models.Places
{
    public class CreateViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [AllowHtml]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Types")]
        [AllowHtml]
        public string Types { get; set; }

        [Required]
        [Display(Name = "Contact")]
        [AllowHtml]
        public string Contact { get; set; }

        [Required]
        [Display(Name = "Country")]
        [AllowHtml]
        public string Country { get; set; }

        [Required]
        [Display(Name = "City")]
        [AllowHtml]
        public string City { get; set; }

        [Display(Name = "Area")]
        [AllowHtml]
        public string Area { get; set; }

        [Required]
        [Display(Name = "Street")]
        [AllowHtml]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Number")]
        [Range(0, 1000)]
        [AllowHtml]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MinLength(10)]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Weekday hours")]
        [AllowHtml]
        public string WeekdayHours { get; set; }

        [Required]
        [Display(Name = "Weekend hours")]
        [AllowHtml]
        public string WeekendHours { get; set; }

        [Required]
        [Display(Name = "Average bill")]
        [AllowHtml]
        public int? AverageBill { get; set; }

        public IEnumerable<SelectListItem> TypeList => new List<SelectListItem>
        {
            new SelectListItem { Text = "Restaurant", Value = "Restaurant"},
            new SelectListItem { Text = "Club", Value = "Club"},
            new SelectListItem { Text = "Cafe", Value = "Cafe"}
        };
    }
}