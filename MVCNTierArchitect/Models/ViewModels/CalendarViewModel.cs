using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCNTierArchitect.Models.ViewModels
{
    public class CalendarViewModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public Nullable<DateTime> End { get; set; } = null;
        public string Color { get; set; }
        public bool IsFullDay { get; set; } = true;
    }
}