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
    public class EFContentRepository : GenericRepository<Content>, IContentDal
    {
        MVCContext context = new MVCContext();

        public List<Content> GetAllByHeading(Expression<Func<Content, bool>> Filter)
        {
            return context.Contents.Include(x => x.Heading).Where(Filter).ToList();
        }
    }
}
