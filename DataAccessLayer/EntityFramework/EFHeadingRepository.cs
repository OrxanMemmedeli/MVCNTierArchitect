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
    public class EFHeadingRepository : GenericRepository<Heading>, IHeadingDal
    {
        MVCContext context = new MVCContext();

        public List<Heading> GetAllWithContentAndWriter()
        {
            return context.Headings.Include(x => x.Contents).Include(x => x.Writer).ToList();
        }

        public List<Heading> GetAllWithContentAndWriter(Expression<Func<Heading, bool>> Filter)
        {
            return context.Headings.Where(Filter).Include(x => x.Contents).Include(x => x.Writer).ToList();
        }
    }
}
