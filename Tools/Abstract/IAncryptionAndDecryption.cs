using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Abstract
{
    public interface IAncryptionAndDecryption
    {
        string EncodeData(string data);
        string DecodeData(string data);

    }
}
