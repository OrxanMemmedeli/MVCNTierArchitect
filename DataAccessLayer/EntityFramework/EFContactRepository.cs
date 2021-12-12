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
    public class EFContactRepository : GenericRepository<Contact>, IContactDal
    {
        MVCContext context = new MVCContext();
        public void DeleteAll(List<Contact> t)
        {
            context.Contacts.RemoveRange(t);
            context.SaveChanges();
        }
    }
}
