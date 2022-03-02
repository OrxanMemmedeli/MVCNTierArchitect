using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShowcaseAPI.Models.Entity
{
    public class Notification
    {
        [Key]
        public int ID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}