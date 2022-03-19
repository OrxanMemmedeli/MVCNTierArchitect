using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IWriterService : IGenericService<Writer>
    {

        bool IsEmailUnique(string email, int? id);
        Writer Get(Expression<Func<Writer, bool>> Filter);
        List<Writer> GetAllWithRole(Expression<Func<Writer, bool>> Filter);
        List<Writer> GetAllWithRole();
    }
}
