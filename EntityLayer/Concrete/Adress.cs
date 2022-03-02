using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntityLayer.Concrete
{
    public class Adress
    {
        [Key]
        public int ID { get; set; }
        [StringLength(500)]
        public string URL { get; set; }
    }
}