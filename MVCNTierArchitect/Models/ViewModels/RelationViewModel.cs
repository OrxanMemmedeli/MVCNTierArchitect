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
    }
}