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
    public class EFMessageRepository : GenericRepository<Message>, IMessageDal
    {
        MVCContext context = new MVCContext();
        public void DeleteAll(List<Message> t)
        {
            foreach (var item in t)
            {
                context.Entry(item).State = EntityState.Deleted;
            }

            context.SaveChanges();
        }
    }
}
