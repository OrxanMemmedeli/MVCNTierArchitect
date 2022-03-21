using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class RoleControllerName
    {
        [ForeignKey("Role")]
        public int? RoleID { get; set; }
        [ForeignKey("ControllerName")]
        public int? ControllerNameID { get; set; }

        public virtual Role Role { get; set; }
        public virtual ControllerName ControllerName { get; set; }
    }
}
