using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShowcaseAPI.Models.Entity
{
    public class Development
    {
        [Key]
        public int ID { get; set; }
        [StringLength(100)]
        public string Heading { get; set; }
        [StringLength(100)]
        public string Icon { get; set; }
        public string Description { get; set; }
    }
}