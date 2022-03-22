using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFAdminRepository : GenericRepository<Admin>, IAdminDal
    {
        MVCContext context = new MVCContext();

        public Admin Get(Expression<Func<Admin, bool>> Filter)
        {
            return context.Admins.FirstOrDefault(Filter);
        }

        public IEnumerable<Admin> GetAllWithRole(Expression<Func<Admin, bool>> Filter)
        {
            return context.Admins.Include(x => x.Role).Where(Filter).ToList();
        }

        public IEnumerable<Admin> GetAllWithRole()
        {
            return context.Admins.Include(x => x.Role).ToList();
        }

        public bool IsUserNameUnique(string username)
        {
            var responce = context.Admins.FirstOrDefault(x => x.UserName == username);
            if (responce != null)
            {
                return false;
            }
            return true;
        }

        public bool IsUserNameUnique(string username, int? id = 0)
        {
            var responce = context.Admins.FirstOrDefault(x => x.UserName == username);
            if (responce != null)
            {
                if (responce.ID == id)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
