using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class RoleControllerNameManager : IRoleControllerNameService
    {
        private readonly IRoleControllerNameDal _roleControllerNameDal;

        public RoleControllerNameManager(IRoleControllerNameDal roleControllerNameDal)
        {
            _roleControllerNameDal = roleControllerNameDal;
        }

        public void Add(RoleControllerName t)
        {
            _roleControllerNameDal.Create(t);
        }

        public void AddRange(List<RoleControllerName> t)
        {
            _roleControllerNameDal.AddRange(t);
        }

        public void Delete(RoleControllerName t)
        {
            _roleControllerNameDal.Delete(t);
        }

        public void DeleteRange(List<RoleControllerName> t)
        {
            _roleControllerNameDal.DeleteRange(t);
        }

        public List<RoleControllerName> GetAll()
        {
            return _roleControllerNameDal.GetAll();
        }

        public List<RoleControllerName> GetAll(Expression<Func<RoleControllerName, bool>> Filter)
        {
            return _roleControllerNameDal.GetAll(Filter);
        }

        public RoleControllerName GetByID(Expression<Func<RoleControllerName, bool>> Filter)
        {
            return _roleControllerNameDal.GetById(Filter);
        }

        public string[] GetRoleControllerNames(int roleID)
        {
            return _roleControllerNameDal.GetRoleControllerNames(roleID);
        }

        public void Update(RoleControllerName t, int id)
        {
            _roleControllerNameDal.Update(t, id);
        }
    }
}
