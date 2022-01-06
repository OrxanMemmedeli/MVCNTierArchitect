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
    public class EFWriterRepository : GenericRepository<Writer>, IWriterDal
    {
        MVCContext context = new MVCContext();
        public bool IsEmailUnique(string email)
        {
            var responce = context.Writers.FirstOrDefault(x => x.Email == email);
            if (responce != null)
            {
                return false;
            }
            return true;
        }
    }
}
