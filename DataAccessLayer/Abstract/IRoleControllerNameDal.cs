using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRoleControllerNameDal : IGenericDal<RoleControllerName>
    {
        void AddRange(List<RoleControllerName> t);
        void DeleteRange(List<RoleControllerName> t);
        string[] GetRoleControllerNames(int roleID);
    }
}
