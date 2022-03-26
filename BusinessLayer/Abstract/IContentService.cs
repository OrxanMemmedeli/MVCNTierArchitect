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
    public interface IContentService : IGenericService<Content>
    {
        List<Content> GetAllByHeading(Expression<Func<Content, bool>> Filter);
        List<Content> GetAllByHeadingAndWriter(Expression<Func<Content, bool>> Filter);
        List<Content> GetAllBySearchModel(Expression<Func<Content, bool>> Filter, HeadingSearchViewModel search);
    }
}
