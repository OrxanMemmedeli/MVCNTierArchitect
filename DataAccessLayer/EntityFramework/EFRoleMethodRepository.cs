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
            using (var cx = new MVCContext())
            {
                var roleMethods = cx.RoleMethods.ToList();

                foreach (var item in t)
                {
                    var deletedEntity = roleMethods.SingleOrDefault(x => x.RoleID == item.RoleID && x.MethodNameID == item.MethodNameID);
                    cx.RoleMethods.Remove(deletedEntity);
                }


                cx.SaveChanges();
            }

        }

        public string[] GetRoleMethodNames(int roleID)
        {
            var roleMethods = context.RoleMethods.Where(x => x.RoleID == roleID).Include(x => x.MethodName);
            string[] names = new string[roleMethods.Count()];
            if (roleMethods != null)
            {
                int index = 0;
                foreach (var item in roleMethods)
                {
                    names[index] = item.MethodName.Name;
                    index++;
                }
            }
            return names;
        }
    }
}
