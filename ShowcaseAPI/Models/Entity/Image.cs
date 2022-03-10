using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShowcaseAPI.Models.Entity
{
    public class Image
    {
        [Key]
        public int ID { get; set; }
        [StringLength(500)]
        public string Title { get; set; }        
        [StringLength(100)]
        public string Name { get; set; }
        public string URL { get; set; }


    }
}