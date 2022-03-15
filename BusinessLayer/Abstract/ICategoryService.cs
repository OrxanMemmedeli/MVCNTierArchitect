using EntityLayer.Concrete;
using EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        List<Category> GetAllWithHeading();
        List<CategoryChartViewModal> GetCountHeading();
        List<CategoryChartViewModal> GetCountHeading(Expression<Func<Category, bool>> Filter);
    }
}
