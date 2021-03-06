using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tools.Abstract;
using Tools.Concrete;

namespace MVCNTierArchitect.Infrastrucrure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {            
        private readonly IKernel _ninjectKernal;
        public NinjectControllerFactory()
        {
            _ninjectKernal = new StandardKernel();
            AddBLLBinding();
            AddToolsBinding();
        }

        private void AddBLLBinding()
        {
            _ninjectKernal.Bind<IAdminService>().To<AdminManager>().WithConstructorArgument("adminDal", new EFAdminRepository());
            _ninjectKernal.Bind<IAboutService>().To<AboutManager>().WithConstructorArgument("aboutDal", new EFAboutRepository());
            _ninjectKernal.Bind<ICategoryService>().To<CategoryManager>().WithConstructorArgument("categoryDal", new EFCategoryRepository());
            _ninjectKernal.Bind<IContactService>().To<ContactManager>().WithConstructorArgument("contactDal", new EFContactRepository());
            _ninjectKernal.Bind<IContentService>().To<ContentManager>().WithConstructorArgument("contentDal", new EFContentRepository());
            _ninjectKernal.Bind<IHeadingService>().To<HeadingManager>().WithConstructorArgument("headingDal", new EFHeadingRepository());
            _ninjectKernal.Bind<IImageFileService>().To<ImageFileManager>().WithConstructorArgument("imageFileDal", new EFImageFileRepository());
            _ninjectKernal.Bind<IMessageService>().To<MessageManager>().WithConstructorArgument("messageDal", new EFMessageRepository());
            _ninjectKernal.Bind<IWriterService>().To<WriterManager>().WithConstructorArgument("writerDal", new EFWriterRepository());
            _ninjectKernal.Bind<ISkillInfoService>().To<SkillInfoManager>().WithConstructorArgument("skillInfoDal", new EFSkillInfoRepository());
            _ninjectKernal.Bind<ISkillService>().To<SkillManager>().WithConstructorArgument("skillDal", new EFSkillRepository());
            _ninjectKernal.Bind<IAdressService>().To<AdressManager>().WithConstructorArgument("adressDal", new EFAdressRepository());
            _ninjectKernal.Bind<IRoleService>().To<RoleManager>().WithConstructorArgument("roleDal", new EFRoleRepository());
            _ninjectKernal.Bind<IMethodNameService>().To<MethodNameManager>().WithConstructorArgument("methodNameDal", new EFMethodNameRepository());
            _ninjectKernal.Bind<IRoleMethodService>().To<RoleMethodManager>().WithConstructorArgument("roleMethodDal", new EFRoleMethodRepository());
            _ninjectKernal.Bind<IRoleControllerNameService>().To<RoleControllerNameManager>().WithConstructorArgument("roleControllerNameDal", new EFRoleControllerNameRepository());
            _ninjectKernal.Bind<IControllerNameService>().To<ControllerNameManager>().WithConstructorArgument("controllerNameDal", new EFControllerNameRepository());
        }

        private void AddToolsBinding()
        {
            _ninjectKernal.Bind<IAncryptionAndDecryption>().To<AncryptionAndDecryption>();
            _ninjectKernal.Bind<ISessionControl>().To<SessionControl>();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernal.Get(controllerType);

        }
    }
}