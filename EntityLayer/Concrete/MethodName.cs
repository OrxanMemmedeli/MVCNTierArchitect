using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class MethodName
    {
        public MethodName()
        {
            this.RoleMethods = new HashSet<RoleMethod>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<RoleMethod> RoleMethods { get; set; }
    }
}
