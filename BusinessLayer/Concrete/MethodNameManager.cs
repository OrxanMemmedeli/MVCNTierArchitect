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
    public class MethodNameManager : IMethodNameService
    {
        private readonly IMethodNameDal _methodNameDal;

        public MethodNameManager(IMethodNameDal methodNameDal)
        {
            _methodNameDal = methodNameDal;
        }

        public void Add(MethodName t)
        {
            _methodNameDal.Create(t);
        }

        public void Delete(MethodName t)
        {
            _methodNameDal.Delete(t);
        }

        public List<MethodName> GetAll()
        {
            return _methodNameDal.GetAll();
        }

        public List<MethodName> GetAll(Expression<Func<MethodName, bool>> Filter)
        {
            return _methodNameDal.GetAll(Filter);
        }

        public MethodName GetByID(Expression<Func<MethodName, bool>> Filter)
        {
            return _methodNameDal.GetById(Filter);
        }

        public void Update(MethodName t, int id)
        {
            _methodNameDal.Update(t,id);
        }
    }
}
