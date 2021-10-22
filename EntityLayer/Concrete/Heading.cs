using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Heading
    {
        public Heading()
        {
            this.Contents = new HashSet<Content>();
        }
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public int CategorID { get; set; }
        public virtual Category Categor { get; set; }

        public int WriterID { get; set; }
        public virtual Writer Writer { get; set; }

        public ICollection<Content> Contents { get; set; }
    }
}
