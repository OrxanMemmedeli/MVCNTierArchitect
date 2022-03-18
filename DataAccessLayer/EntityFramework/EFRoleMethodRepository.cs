using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFRoleMethodRepository : GenericRepository<RoleMethod>, IRoleMethodDal
    {
        MVCContext context = new MVCContext();

        public void AddRange(List<RoleMethod> t)
        {
            //var addedEntity = context.Entry(t);
            //addedEntity.State = EntityState.Added;
            context.RoleMethods.AddRange(t);
            context.SaveChanges();
        }

        public void DeleteRange(List<RoleMethod> t)
        {
            foreach (var item in t)
            {
                var deletedEntity = context.Entry(item);
                deletedEntity.State = EntityState.Deleted;
            }

            context.SaveChanges();
        }
    }
}
