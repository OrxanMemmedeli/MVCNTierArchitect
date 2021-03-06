using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        MVCContext context = new MVCContext();
        DbSet<T> _object;

        public GenericRepository()
        {
            _object = context.Set<T>();
        }

        public void Create(T t)
        {
            var addedEntity = context.Entry(t);
            addedEntity.State = EntityState.Added;
            //_object.Add(t);
            context.SaveChanges();
        }

        public void Delete(T t)
        {
            var deletedEntity = context.Entry(t);
            deletedEntity.State = EntityState.Deleted;
            //_object.Remove(t);
            context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _object.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> Filter)
        {
            return _object.Where(Filter).ToList();
        }

        public T GetById(Expression<Func<T, bool>> Filter)
        {
            return _object.FirstOrDefault(Filter);
        }

        public void Update(T t, int id)
        {
            var updatedEntity = context.Entry(t);

            var local = _object.Find(id);
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            updatedEntity.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
