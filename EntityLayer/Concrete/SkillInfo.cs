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
    public class SkillInfo
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string About { get; set; }
        public string ImageURL { get; set; }
        [NotMapped]
        public virtual HttpPostedFileBase imageFile { get; set; }
    }
}
