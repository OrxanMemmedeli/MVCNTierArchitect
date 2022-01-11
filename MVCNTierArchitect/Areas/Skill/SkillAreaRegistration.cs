using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Skill
{
    public class SkillAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Skill";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Skill_default",
                "Skill/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}