﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IWriterDal : IGenericDal<Writer>
    {
        bool IsEmailUnique(string email);
        Writer Get(Expression<Func<Writer, bool>> Filter);

    }
}
