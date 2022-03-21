using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCNTierArchitect.Models.ViewModels
{
    public class RelationViewModel
    {
        public List<MethodName> MethodNames { get; set; }
        public List<RoleMethod> RoleMethods { get; set; }
        public List<Role> Roles { get; set; }

        public List<ControllerName> ControllerNames { get; set; }
        public List<RoleControllerName> RoleControllerNames { get; set; }
    }
}