using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRoleMethodDal :IGenericDal<RoleMethod>
    {
        void AddRange(List<RoleMethod> t);
        void DeleteRange(List<RoleMethod> t);
        string[] GetRoleMethodNames(int roleID);
    }
}
