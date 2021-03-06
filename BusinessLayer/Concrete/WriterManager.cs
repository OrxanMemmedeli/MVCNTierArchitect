using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class WriterManager : IWriterService
    {
        private readonly IWriterDal _writerDal;
        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public void Add(Writer t)
        {
            _writerDal.Create(t);
        }

        public void Delete(Writer t)
        {
            _writerDal.Delete(t);
        }

        public Writer Get(Expression<Func<Writer, bool>> Filter)
        {
            return _writerDal.Get(Filter);
        }

        public List<Writer> GetAll()
        {
            return _writerDal.GetAll();
        }

        public List<Writer> GetAll(Expression<Func<Writer, bool>> Filter)
        {
            return _writerDal.GetAll(Filter);
        }

        public IEnumerable<Writer> GetAllWithRole(Expression<Func<Writer, bool>> Filter)
        {
            return _writerDal.GetAllWithRole(Filter);
        }

        public IEnumerable<Writer> GetAllWithRole()
        {
            return _writerDal.GetAllWithRole();
        }

        public Writer GetByID(Expression<Func<Writer, bool>> Filter)
        {
            return _writerDal.GetById(Filter);
        }

        public bool IsEmailUnique(string email, int? id)
        {
            return _writerDal.IsEmailUnique(email, id);
        }

        public void Update(Writer t, int id)
        {
            _writerDal.Update(t, id);
        }
    }
}
