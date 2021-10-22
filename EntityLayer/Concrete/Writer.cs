using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [StringLength(250)]
        public string Email { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public virtual string ConfirmPassword { get; set; }

        public ICollection<Heading> Headings { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
}
