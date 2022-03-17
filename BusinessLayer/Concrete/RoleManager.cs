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
    public class RoleManager : IRoleService
    {
        private readonly IRoleDal _roleDal;

        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        public void Add(Role t)
        {
            _roleDal.Create(t);
        }

        public void Delete(Role t)
        {
            _roleDal.Delete(t);
        }

        public List<Role> GetAll()
        {
            return _roleDal.GetAll();
        }

        public List<Role> GetAll(Expression<Func<Role, bool>> Filter)
        {
            return _roleDal.GetAll(Filter);
        }

        public Role GetByID(Expression<Func<Role, bool>> Filter)
        {
            return _roleDal.GetById(Filter);
        }

        public void Update(Role t, int id)
        {
            _roleDal.Update(t, id);
        }
    }
}
