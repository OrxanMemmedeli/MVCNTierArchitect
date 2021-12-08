using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string SenderEmail { get; set; }
        [StringLength(250)]
        public string ReceiverEmail { get; set; }
        [StringLength(50)]
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsReaded { get; set; } = false;
        public bool IsResponded { get; set; } = false;
    }
}
