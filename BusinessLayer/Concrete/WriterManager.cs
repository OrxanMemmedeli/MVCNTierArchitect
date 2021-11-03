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

        public List<Writer> GetAll()
        {
            return _writerDal.GetAll();
        }

        public List<Writer> GetAll(Expression<Func<Writer, bool>> Filter)
        {
            return _writerDal.GetAll(Filter);
        }

        public Writer GetByID(Expression<Func<Writer, bool>> Filter)
        {
            return _writerDal.GetById(Filter);
        }

        public void Update(Writer t)
        {
            _writerDal.Update(t);
        }
    }
}
