using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Services
{
    public class MD5Service
    {
        public byte[] GetMD5Hash(byte[] sourcebuffer)
        {
            var md5 = MD5.Create();
            return md5.ComputeHash(sourcebuffer);
        }

        public bool VerifyMD5(byte[] sourceBuffer, byte[] computedHash)
        {
            return computedHash == GetMD5Hash(computedHash);
        }
    }
}
