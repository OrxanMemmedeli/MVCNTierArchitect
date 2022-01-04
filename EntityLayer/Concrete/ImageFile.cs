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
    public class ImageFile
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public string URL { get; set; }

        [NotMapped]
        public List<HttpPostedFileBase> Images{ get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
    }
}
