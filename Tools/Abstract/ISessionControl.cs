using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Abstract
{
    public interface ISessionControl
    {
        int GetAdminID(string userName);
        int GetWriterID(string email);
    }
}
