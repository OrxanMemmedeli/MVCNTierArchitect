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
    public class EFRoleControllerNameRepository : GenericRepository<RoleControllerName>, IRoleControllerNameDal
    {
        MVCContext context = new MVCContext();

        public void AddRange(List<RoleControllerName> t)
        {
            context.RoleControllerNames.AddRange(t);
            context.SaveChanges();
        }

        public void DeleteRange(List<RoleControllerName> t)
        {
            using (var cx = new MVCContext())
            {
                var roleControllerNames = cx.RoleControllerNames.ToList();

                foreach (var item in t)
                {
                    var deletedEntity = roleControllerNames.SingleOrDefault(x => x.RoleID == item.RoleID && x.ControllerNameID == item.ControllerNameID);
                    cx.RoleControllerNames.Remove(deletedEntity);
                }


                cx.SaveChanges();
            }
        }

        public string[] GetRoleControllerNames(int roleID)
        {
            var roleControllerNames = context.RoleControllerNames.Where(x => x.RoleID == roleID).Include(x => x.ControllerName);
            string[] names = new string[roleControllerNames.Count()];
            if (roleControllerNames != null)
            {
                int index = 0;
                foreach (var item in roleControllerNames)
                {
                    names[index] = item.ControllerName.Name.ToLower();
                    index++;
                }
            }
            return names;
        }
    }
}
