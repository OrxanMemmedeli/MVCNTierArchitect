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
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public void Add(Contact t)
        {
            _contactDal.Create(t);
        }

        public void Delete(Contact t)
        {
            _contactDal.Delete(t);
        }

        public List<Contact> GetAll()
        {
            return _contactDal.GetAll();
        }

        public List<Contact> GetAll(Expression<Func<Contact, bool>> Filter)
        {
            return _contactDal.GetAll(Filter);
        }

        public Contact GetByID(Expression<Func<Contact, bool>> Filter)
        {
            return _contactDal.GetById(Filter);
        }

        public void Update(Contact t)
        {
            _contactDal.Update(t);
        }
    }
}
