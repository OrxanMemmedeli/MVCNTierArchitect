using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Content
    {
        [Key]
        public int ID { get; set; }
        [StringLength(1000)]
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; } = true;

        public int HeadingID { get; set; }
        public virtual Heading Heading { get; set; }

        public int? WriterID { get; set; }
        public virtual Writer Writer { get; set; }
    }
}
