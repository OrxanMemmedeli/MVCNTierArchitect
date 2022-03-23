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
    public class Writer
    {
        public Writer()
        {
            this.Headings = new HashSet<Heading>();
            this.Contents = new HashSet<Content>();
        }
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        [StringLength(250)]
        public string ImageURL { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [StringLength(100)] 
        public string About { get; set; }
        public bool Status { get; set; }
        [NotMapped]
        public virtual string ConfirmPassword { get; set; }
        [NotMapped]
        public string OldPassword { get; set; }

        [NotMapped] 
        public virtual HttpPostedFileBase imageFile { get; set; }

        [ForeignKey("Role")]
        public int? RoleID { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Heading> Headings { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}
