using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Role
    {
        public Role()
        {
            this.RoleControllerNames = new HashSet<RoleControllerName>();
            this.RoleMethods = new HashSet<RoleMethod>();
            this.Writers = new HashSet<Writer>();
            this.Admins = new HashSet<Admin>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<RoleMethod> RoleMethods { get; set; }
        public virtual ICollection<RoleControllerName> RoleControllerNames { get; set; }
        public virtual ICollection<Writer> Writers { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
    }
}
