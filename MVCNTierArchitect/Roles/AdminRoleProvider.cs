using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Tools.Concrete;

namespace MVCNTierArchitect.Roles
{
    public class AdminRoleProvider : RoleProvider
    {
        //private readonly IAdminService _adminService;

        //public AdminRoleProvider(IAdminService adminService)
        //{
        //    _adminService = adminService;
        //}

        private readonly AdminManager adminManager = new AdminManager(new EFAdminRepository());
        private readonly AncryptionAndDecryption ancryptionAndDecryption = new AncryptionAndDecryption();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //var admin = _adminService.Get(x => x.UserName == username);
            var admin = adminManager.Get(x => x.UserName == username);
            if (admin != null)
            {
                return new string[] { admin.Role };
            }
            return new string[] { "Error" };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}