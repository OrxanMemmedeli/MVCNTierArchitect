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
    public class AdminManager : IAdminService
    {
        private readonly IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public void Add(Admin t)
        {
            _adminDal.Create(t);
        }

        public void Delete(Admin t)
        {
            _adminDal.Delete(t);
        }

        public List<Admin> GetAll()
        {
            return _adminDal.GetAll();
        }

        public List<Admin> GetAll(Expression<Func<Admin, bool>> Filter)
        {
            return _adminDal.GetAll(Filter);
        }

        public Admin GetByID(Expression<Func<Admin, bool>> Filter)
        {
            return _adminDal.GetById(Filter);
        }

        public void Update(Admin t)
        {
            _adminDal.Update(t);
        }
    }
}
