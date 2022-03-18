using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRoleMethodService : IGenericService<RoleMethod>
    {
        void AddRange(List<RoleMethod> t);
        void DeleteRange(List<RoleMethod> t);

    }
}
