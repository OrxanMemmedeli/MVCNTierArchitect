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
    public class EFHeadingRepository : GenericRepository<Heading>, IHeadingDal
    {
        MVCContext context = new MVCContext();

        public List<Heading> GetAllWithContent()
        {
            return context.Headings.Include(x => x.Contents).ToList();
        }
    }
}
