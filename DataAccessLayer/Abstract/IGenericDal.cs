using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T>
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> Filter);
        void Create(T t);
        void Update(T t, int id);
        void Delete(T t);
        T GetById(Expression<Func<T, bool>> Filter);
    }
}
