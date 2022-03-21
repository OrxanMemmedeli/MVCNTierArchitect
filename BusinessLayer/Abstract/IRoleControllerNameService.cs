using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRoleControllerNameService : IGenericService<RoleControllerName>
    {
        void AddRange(List<RoleControllerName> t);
        void DeleteRange(List<RoleControllerName> t);
        string[] GetRoleControllerNames(int roleID);
    }
}
