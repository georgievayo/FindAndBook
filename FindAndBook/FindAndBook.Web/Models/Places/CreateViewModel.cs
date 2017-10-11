using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FindAndBook.Web.Models.Places
{
    public class CreateViewModel
    {
        [Required]
        [MinLength(4)]
        [Display(Name = "Name")]
        [AllowHtml]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        [AllowHtml]
        public string Types { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Contact")]
        [AllowHtml]
        public string Contact { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Country")]
        [AllowHtml]
        public string Country { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "City")]
        [AllowHtml]
        public string City { get; set; }

        [MinLength(3)]
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
        [RegularExpression("[0-9]{2}:[0-9]{2} - [0-9]{2}:[0-9]{2}")]
        [Display(Name = "Weekday hours")]
        [AllowHtml]
        public string WeekdayHours { get; set; }

        [Required]
        [RegularExpression("[0-9]{2}:[0-9]{2} - [0-9]{2}:[0-9]{2}")]
        [Display(Name = "Weekend hours")]
        [AllowHtml]
        public string WeekendHours { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Average bill")]
        [AllowHtml]
        public int? AverageBill { get; set; }

        [Required]
        [Display(Name = "Tables for 2 people")]
        [AllowHtml]
        public int TwoPeopleCount { get; set; }

        [Required]
        [Display(Name = "Tables for 4 people")]
        [AllowHtml]
        public int FourPeopleCount { get; set; }

        [Required]
        [Display(Name = "Tables for 6 people")]
        [AllowHtml]
        public int SixPeopleCount { get; set; }

        public IEnumerable<SelectListItem> TypeList => new List<SelectListItem>
        {
            new SelectListItem { Text = "Restaurant", Value = "Restaurant"},
            new SelectListItem { Text = "Club", Value = "Club"},
            new SelectListItem { Text = "Cafe", Value = "Cafe"}
        };
    }
}