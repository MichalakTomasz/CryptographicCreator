using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                var md5 = MD5.Create();
                return md5.ComputeHash(sourcebuffer);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"MD5 service GetMD5Hash error: {e.Message}");
                return default(byte[]);
            }
            
        }

        public bool VerifyMD5(byte[] sourceBuffer, byte[] computedHash)
        {
            var md5 = MD5.Create();
            var newHash = GetMD5Hash(sourceBuffer);
            int i = 0;
            while (i < computedHash.Length && computedHash[i] == newHash[i]) i++;
            return i == computedHash.Length;
        }
    }
}
