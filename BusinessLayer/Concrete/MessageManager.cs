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
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void Add(Message t)
        {
            _messageDal.Create(t);
        }

        public void Delete(Message t)
        {
            _messageDal.Delete(t);
        }

        public void DeleteAll(List<Message> t)
        {
            _messageDal.DeleteAll(t);
        }

        public List<Message> GetAll()
        {
            return _messageDal.GetAll();
        }

        public List<Message> GetAll(Expression<Func<Message, bool>> Filter)
        {
            return _messageDal.GetAll(Filter);
        }

        public Message GetByID(Expression<Func<Message, bool>> Filter)
        {
            return _messageDal.GetById(Filter);
        }

        public void Update(Message t, int id)
        {
            _messageDal.Update(t, id);
        }
    }
}
