using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class Place
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string WeekendHours { get; set; }

        public string WeekdayHours { get; set; }

        public string Details { get; set; }

        public int? AverageBill { get; set; }
    }
}
