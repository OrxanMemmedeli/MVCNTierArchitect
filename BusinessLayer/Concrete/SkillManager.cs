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
    public class SkillManager : ISkillService
    {
        private readonly ISkillDal _skillDal;

        public SkillManager(ISkillDal skillDal)
        {
            _skillDal = skillDal;
        }

        public void Add(Skill t)
        {
            _skillDal.Create(t);
        }

        public void Delete(Skill t)
        {
            _skillDal.Delete(t);
        }

        public List<Skill> GetAll()
        {
            return _skillDal.GetAll();
        }

        public List<Skill> GetAll(Expression<Func<Skill, bool>> Filter)
        {
            return _skillDal.GetAll(Filter);
        }

        public Skill GetByID(Expression<Func<Skill, bool>> Filter)
        {
            return _skillDal.GetById(Filter);
        }

        public void Update(Skill t, int id)
        {
            _skillDal.Update(t, id);
        }
    }
}
