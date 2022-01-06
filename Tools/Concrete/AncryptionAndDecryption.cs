using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tools.Abstract;

namespace Tools.Concrete
{
    public class AncryptionAndDecryption : IAncryptionAndDecryption
    {
        public string EncodeData(string data)
        {
            string returndata = "";
            byte[] encoding = UTF8Encoding.UTF8.GetBytes(data);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("!@#$%^&*"));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] resuts = transform.TransformFinalBlock(encoding, 0, data.Length);
                    returndata = Convert.ToBase64String(resuts, 0, resuts.Length);
                }
            }
            return returndata;
        }

        public string DecodeData(string data)
        {
            string returndata = "";
            byte[] str = Convert.FromBase64String(data);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("!@#$%^&*"));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] resuts = transform.TransformFinalBlock(str, 0, data.Length);
                    returndata = UTF8Encoding.UTF8.GetString(resuts);
                }
            }
            return returndata;
        }
    }
}
