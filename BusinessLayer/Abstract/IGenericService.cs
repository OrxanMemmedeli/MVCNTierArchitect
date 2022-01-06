using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T>
    {
        void Add(T t);
        void Update(T t, int id);
        void Delete(T t);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> Filter);
        T GetByID(Expression<Func<T, bool>> Filter);

    }
}
