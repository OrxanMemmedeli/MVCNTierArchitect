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
    public class EFCategoryRepository : GenericRepository<Category>, ICategoryDal
    {
        MVCContext context = new MVCContext();
        public List<Category> GetAllWithHeading()
        {
            return context.Categories.Include(x => x.Headings).ToList();
        }

        public List<CategoryChartViewModal> GetCountHeading()
        {
            List<CategoryChartViewModal> list = new List<CategoryChartViewModal>();
            var categories = context.Categories.Include(x => x.Headings);
            foreach (var item in categories)
            {
                list.Add(new CategoryChartViewModal()
                {
                    CategoryName = item.Name,
                    HeadingCount = item.Headings.Count()
                });
            }
            return list;
        }

        public List<CategoryChartViewModal> GetCountHeading(Expression<Func<Category, bool>> Filter)
        {
            List<CategoryChartViewModal> list = new List<CategoryChartViewModal>();
            var categories = context.Categories.Include(x => x.Headings).Where(Filter);
            foreach (var item in categories)
            {
                list.Add(new CategoryChartViewModal()
                {
                    CategoryName = item.Name,
                    HeadingCount = item.Headings.Count()
                });
            }
            return list;
        }
    }
}
