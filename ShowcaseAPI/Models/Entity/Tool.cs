using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShowcaseAPI.Models.Entity
{
    public class Tool
    {
        [Key]
        public int ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public double Percent { get; set; }
        [StringLength(100)]
        public string Type { get; set; }
    }
}