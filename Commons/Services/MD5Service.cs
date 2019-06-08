using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Commons
{
    public class MD5Service : IHashService
    {
        public byte[] GetHash(byte[] sourcebuffer)
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

        public bool VerifyHash(byte[] sourceBuffer, byte[] hash)
        {
            var newHash = GetHash(sourceBuffer);
            if (newHash.Length == hash.Length)
            {
                int i = 0;
                while (i < hash.Length && hash[i] == newHash[i]) i++;
                return i == hash.Length;
            }
            else return false;
        }
    }
}
