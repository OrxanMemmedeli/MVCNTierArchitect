using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Skill.Controllers
{
    [RouteArea("Skill")]
    public class SkillController : Controller
    {
        private readonly ISkillService _skillService;
        private readonly ISkillInfoService _skillInfoService;

        public SkillController(ISkillService skillService, ISkillInfoService skillInfoService)
        {
            _skillService = skillService;
            _skillInfoService = skillInfoService;
        }
        
        // GET: Skill/Skill
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult About()
        {
            var about = _skillInfoService.GetAll().Take(1);
            return PartialView(about);
        }

        public PartialViewResult Skills()
        {
            var skills = _skillService.GetAll();
            return PartialView(skills);
        }

        public ActionResult Details()
        {
            return View();
        }

        public PartialViewResult SkillInfo()
        {
            var about = _skillInfoService.GetAll();
            return PartialView(about);
        }

        public PartialViewResult Skill()
        {
            var skills = _skillService.GetAll();
            return PartialView(skills);
        }

        public ActionResult CreateSkill()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSkill(EntityLayer.Concrete.Skill skill)
        {
            _skillService.Add(skill);
            return RedirectToAction("Details");
        }

        public ActionResult CreateInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInfo(SkillInfo skillInfo)
        {
            _skillInfoService.Add(skillInfo);
            return RedirectToAction("Details");
        }

        public ActionResult EditSkill(int id)
        {
            var skill = _skillService.GetByID(x => x.ID == id);
            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSkill(EntityLayer.Concrete.Skill skill)
        {
            _skillService.Update(skill,skill.ID);
            return RedirectToAction("Details");
        }

        public ActionResult EditInfo(int id)
        {
            var skillInfo = _skillInfoService.GetByID(x => x.ID == id);
            return View(skillInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfo(SkillInfo skillInfo)
        {
            _skillInfoService.Update(skillInfo, skillInfo.ID);
            return RedirectToAction("Details");
        }

        public ActionResult DeleteInfo(int id)
        {
            var skillInfo = _skillInfoService.GetByID(x => x.ID == id);
            _skillInfoService.Delete(skillInfo);
            return RedirectToAction("Details");
        }

        public ActionResult DeleteSkill(int id)
        {
            var skill = _skillService.GetByID(x => x.ID == id);
            _skillService.Delete(skill);
            return RedirectToAction("Details");
        }
    }
}