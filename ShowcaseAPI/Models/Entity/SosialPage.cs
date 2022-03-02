using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShowcaseAPI.Models.Entity
{
    public class SosialPage
    {
        [Key]
        public int ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string PageName { get; set; }
        public string URL { get; set; }
        [StringLength(100)]
        public string Icon { get; set; }
    }
}