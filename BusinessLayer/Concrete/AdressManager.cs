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
    public class AdressManager : IAdressService
    {
        private readonly IAdressDal _adressDal;

        public AdressManager(IAdressDal adressDal)
        {
            _adressDal = adressDal;
        }

        public void Add(Adress t)
        {
            _adressDal.Create(t);
        }

        public void Delete(Adress t)
        {
            _adressDal.Delete(t);
        }

        public List<Adress> GetAll()
        {
            return _adressDal.GetAll();
        }

        public List<Adress> GetAll(Expression<Func<Adress, bool>> Filter)
        {
            return _adressDal.GetAll(Filter);
        }

        public Adress GetByID(Expression<Func<Adress, bool>> Filter)
        {
            return _adressDal.GetById(Filter);
        }

        public void Update(Adress t, int id)
        {
            _adressDal.Update(t, t.ID);
        }
    }
}
