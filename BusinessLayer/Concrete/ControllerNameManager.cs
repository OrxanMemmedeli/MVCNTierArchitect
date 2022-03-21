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
    public class ControllerNameManager : IControllerNameService
    {
        private readonly IControllerNameDal _controllerNameDal;

        public ControllerNameManager(IControllerNameDal controllerNameDal)
        {
            _controllerNameDal = controllerNameDal;
        }

        public void Add(ControllerName t)
        {
            _controllerNameDal.Create(t);
        }

        public void Delete(ControllerName t)
        {
            _controllerNameDal.Delete(t);
        }

        public List<ControllerName> GetAll()
        {
            return _controllerNameDal.GetAll();
        }

        public List<ControllerName> GetAll(Expression<Func<ControllerName, bool>> Filter)
        {
            return _controllerNameDal.GetAll(Filter);
        }

        public ControllerName GetByID(Expression<Func<ControllerName, bool>> Filter)
        {
            return _controllerNameDal.GetById(Filter);
        }

        public void Update(ControllerName t, int id)
        {
            _controllerNameDal.Update(t,id);
        }
    }
}
