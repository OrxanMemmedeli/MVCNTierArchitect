﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService : IGenericService<Admin>
    {
        bool IsUserNameUnique(string username);
        Admin Get(Expression<Func<Admin, bool>> Filter);
        List<Admin> GetAllWithRole(Expression<Func<Admin, bool>> Filter);
        List<Admin> GetAllWithRole();
    }
}
