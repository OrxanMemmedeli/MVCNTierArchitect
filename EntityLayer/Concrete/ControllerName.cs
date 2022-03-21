using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public  class ControllerName
    {
        public ControllerName()
        {
            this.RoleControllerNames = new HashSet<RoleControllerName>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)] 
        public string Description { get; set; }

        public virtual ICollection<RoleControllerName> RoleControllerNames { get; set; }
    }
}
