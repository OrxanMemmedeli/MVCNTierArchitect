using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IHeadingService : IGenericService<Heading>
    {
        List<Heading> GetAllWithContentAndWriter();
        List<Heading> GetAllWithContentAndWriter(Expression<Func<Heading, bool>> Filter);

    }
}
