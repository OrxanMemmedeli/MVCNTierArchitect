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
    public class HeadingManager : IHeadingService
    {
        private readonly IHeadingDal _headingDal;

        public HeadingManager(IHeadingDal headingDal)
        {
            _headingDal = headingDal;
        }

        public void Add(Heading t)
        {
            _headingDal.Create(t);
        }

        public void Delete(Heading t)
        {
            _headingDal.Delete(t);
        }

        public List<Heading> GetAll()
        {
            return _headingDal.GetAll();
        }

        public List<Heading> GetAll(Expression<Func<Heading, bool>> Filter)
        {
            return _headingDal.GetAll(Filter);
        }

        public List<Heading> GetAllWithContent()
        {
            return _headingDal.GetAllWithContent();
        }

        public Heading GetByID(Expression<Func<Heading, bool>> Filter)
        {
            return _headingDal.GetById(Filter);
        }

        public void Update(Heading t, int id)
        {
            _headingDal.Update(t, id);
        }
    }
}
