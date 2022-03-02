using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShowcaseAPI.Models.Entity
{
    public class About
    {
        [Key]
        public int ID { get; set; }
        [StringLength(3000)]
        [AllowHtml]
        public string Adress { get; set; }
        [StringLength(3000)]
        [AllowHtml]
        public string Email { get; set; }
        [StringLength(3000)]
        [AllowHtml]
        public string Number { get; set; }
        [AllowHtml]
        public string Map { get; set; }
    }
}