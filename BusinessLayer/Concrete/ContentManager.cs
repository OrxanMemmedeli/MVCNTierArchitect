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
    public class ContentManager : IContentService
    {
        private readonly IContentDal _contentDal;
        public ContentManager(IContentDal contentDal)
        {
            _contentDal = contentDal;
        }

        public void Add(Content t)
        {
            _contentDal.Create(t);
        }

        public void Delete(Content t)
        {
            _contentDal.Delete(t);
        }

        public List<Content> GetAll()
        {
            return _contentDal.GetAll();
        }

        public List<Content> GetAll(Expression<Func<Content, bool>> Filter)
        {
            return _contentDal.GetAll(Filter);
        }

        public Content GetByID(Expression<Func<Content, bool>> Filter)
        {
            return _contentDal.GetById(Filter);
        }

        public void Update(Content t, int id)
        {
            _contentDal.Update(t, id);
        }
    }
}
