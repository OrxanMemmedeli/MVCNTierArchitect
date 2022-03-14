using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.ViewModels;
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

        public List<Content> GetAllBySearchModel(Expression<Func<Content, bool>> Filter, HeadingSearchViewModel search)
        {
            var contents = context.Contents.Include(x => x.Heading).Where(Filter);

            if (!string.IsNullOrEmpty(search.Writer) || !string.IsNullOrWhiteSpace(search.Writer))
            {
                contents = contents.Where(x => x.Writer.Name.Contains(search.Writer.Trim()) || x.Writer.Surname.Contains(search.Writer.Trim()));
            }

            if (!string.IsNullOrEmpty(search.Content) || !string.IsNullOrWhiteSpace(search.Content))
            {
                contents = contents.Where(x => x.Text.Contains(search.Content.Trim()));
            }

            if (search.Date.ToString("dd/MM/yyyy") != "01/01/0001")
            {
                contents = contents.Where(x => x.Writer.Name.Contains(search.Writer.Trim()));
            }

            return contents.ToList();
        }
    }
}
