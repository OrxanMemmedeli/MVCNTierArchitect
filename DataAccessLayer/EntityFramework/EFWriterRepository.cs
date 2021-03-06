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
    public class EFWriterRepository : GenericRepository<Writer>, IWriterDal
    {
        MVCContext context = new MVCContext();
        public Writer Get(Expression<Func<Writer, bool>> Filter)
        {
            return context.Writers.FirstOrDefault(Filter);
        }

        public IEnumerable<Writer> GetAllWithRole(Expression<Func<Writer, bool>> Filter)
        {
            return context.Writers.Include(x => x.Role).Where(Filter).ToList();
        }

        public IEnumerable<Writer> GetAllWithRole()
        {
            return context.Writers.Include(x => x.Role).ToList();
        }

        public bool IsEmailUnique(string email, int? id)
        {
            var responce = context.Writers.FirstOrDefault(x => x.Email == email);
            if (responce != null)
            {
                if (id != null && responce.ID == id)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
