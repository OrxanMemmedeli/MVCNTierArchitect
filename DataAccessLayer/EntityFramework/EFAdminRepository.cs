using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFAdminRepository : GenericRepository<Admin>, IAdminDal
    {
        MVCContext context = new MVCContext();
        public bool IsUserNameUnique(string username)
        {
            var responce = context.Admins.FirstOrDefault(x => x.UserName == username);
            if (responce != null)
            {
                return false;
            }
            return true;
        }
    }
}
