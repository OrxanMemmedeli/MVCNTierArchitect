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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category t)
        {
            _categoryDal.Create(t);
        }

        public void Delete(Category t)
        {
            _categoryDal.Delete(t);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }

        public List<Category> GetAll(Expression<Func<Category, bool>> Filter)
        {
            return _categoryDal.GetAll(Filter);
        }

        public Category GetByID(Expression<Func<Category, bool>> Filter)
        {
            return _categoryDal.GetById(Filter);
        }

        public void Update(Category t)
        {
            _categoryDal.Update(t);
        }
    }
}
