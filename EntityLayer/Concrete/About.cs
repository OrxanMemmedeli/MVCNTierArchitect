using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EntityLayer.Concrete
{
    public class About
    {
        [Key]
        public int ID { get; set; }
        [StringLength(1000)]
        public string DetailsFirst { get; set; }
        [StringLength(1000)]
        public string DetailsSecond { get; set; }
        [StringLength(250)]
        public string İmageFirst { get; set; }
        [StringLength(250)]
        public string İmageSecond { get; set; }
        public bool Status { get; set; } = true;

        [NotMapped]
        public virtual HttpPostedFileBase imageFileFirst { get; set; }
        [NotMapped]
        public virtual HttpPostedFileBase imageFileSecond { get; set; }

    }
}
