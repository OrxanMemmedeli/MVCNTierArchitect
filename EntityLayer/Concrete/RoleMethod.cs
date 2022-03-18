using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class RoleMethod
    {

        [ForeignKey("Role")]
        public int? RoleID { get; set; }
        [ForeignKey("MethodName")] 
        public int? MethodNameID { get; set; }

        public virtual Role Role { get; set; }
        public virtual MethodName MethodName { get; set; }
    }
}
