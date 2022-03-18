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
    public class RoleMethodManager : IRoleMethodService
    {
        private readonly IRoleMethodDal _roleMethodDal;

        public RoleMethodManager(IRoleMethodDal roleMethodDal)
        {
            _roleMethodDal = roleMethodDal;
        }

        public void Add(RoleMethod t)
        {
            _roleMethodDal.Create(t);
        }

        public void AddRange(List<RoleMethod> t)
        {
            _roleMethodDal.AddRange(t);
        }

        public void Delete(RoleMethod t)
        {
            _roleMethodDal.Delete(t);
        }

        public void DeleteRange(List<RoleMethod> t)
        {
            _roleMethodDal.DeleteRange(t);
        }

        public List<RoleMethod> GetAll()
        {
            return _roleMethodDal.GetAll();
        }

        public List<RoleMethod> GetAll(Expression<Func<RoleMethod, bool>> Filter)
        {
            return _roleMethodDal.GetAll(Filter);
        }

        public RoleMethod GetByID(Expression<Func<RoleMethod, bool>> Filter)
        {
            return _roleMethodDal.GetById(Filter);
        }

        public void Update(RoleMethod t, int id)
        {
            _roleMethodDal.Update(t,id);
        }
    }
}
