using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Abstract;



namespace Tools.Concrete
{
    public class SessionControl : ISessionControl
    {
        private readonly IAdminService _adminService;
        private readonly IWriterService _writerService;
        private readonly IAncryptionAndDecryption _ancryptionAndDecryption;

        public SessionControl(IAdminService adminService, IWriterService writerService, IAncryptionAndDecryption ancryptionAndDecryption)
        {
            _adminService = adminService;
            _writerService = writerService;
            _ancryptionAndDecryption = ancryptionAndDecryption;
        }

        public int GetAdminID(string userName)
        {
            var admin = _adminService.Get(x => x.UserName == _ancryptionAndDecryption.EncodeData(userName));
            return admin.ID;
        }

        public int GetWriterID(string email)
        {
            var writer = _writerService.Get(x => x.Email == _ancryptionAndDecryption.EncodeData(email));
            return writer.ID;
        }
    }
}
