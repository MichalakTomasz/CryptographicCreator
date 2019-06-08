using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Commons.Services
{
    public class SHA512CngService : IHashService
    {
        public byte[] GetHash(byte[] sourceBuffer)
        {
            try
            {
                var sha256Cng = SHA512Cng.Create();
                return sha256Cng.ComputeHash(sourceBuffer);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"SHA512Cng service GetSHA512CngHash error: {e.Message}");
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
