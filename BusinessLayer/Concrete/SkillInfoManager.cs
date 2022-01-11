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
    public class SkillInfoManager : ISkillInfoService
    {
        private readonly ISkillInfoDal _skillInfoDal;

        public SkillInfoManager(ISkillInfoDal skillInfoDal)
        {
            _skillInfoDal = skillInfoDal;
        }

        public void Add(SkillInfo t)
        {
            _skillInfoDal.Create(t);
        }

        public void Delete(SkillInfo t)
        {
            _skillInfoDal.Delete(t);
        }

        public List<SkillInfo> GetAll()
        {
            return _skillInfoDal.GetAll();
        }

        public List<SkillInfo> GetAll(Expression<Func<SkillInfo, bool>> Filter)
        {
            return _skillInfoDal.GetAll(Filter);
        }

        public SkillInfo GetByID(Expression<Func<SkillInfo, bool>> Filter)
        {
            return _skillInfoDal.GetById(Filter);
        }

        public void Update(SkillInfo t, int id)
        {
            _skillInfoDal.Update(t, id);
        }
    }
}
