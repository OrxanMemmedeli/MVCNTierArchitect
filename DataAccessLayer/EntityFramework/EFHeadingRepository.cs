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
  
        public List<Heading> GetAllWithContentAndWriter()
        {
            using (var cx = new MVCContext())
            {
                var headings = cx.Headings.Include(x => x.Contents).Include(x => x.Writer).Include(x => x.Category).ToList();
                return headings;
            }

        }

        public List<Heading> GetAllWithContentAndWriter(Expression<Func<Heading, bool>> Filter)
        {
            using (var cx = new MVCContext())
            {
                var headings = cx.Headings.Where(Filter).Include(x => x.Contents).Include(x => x.Writer).Include(x => x.Category).ToList();
                return headings;
            }
        }
    }
}
