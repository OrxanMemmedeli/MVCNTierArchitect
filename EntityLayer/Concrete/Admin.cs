using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public virtual string ConfirmPassword { get; set; }
        [StringLength(1)] 
        public string Role { get; set; }
        public bool Status { get; set; } = true;
    }
}
