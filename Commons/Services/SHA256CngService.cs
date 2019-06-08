using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Commons
{
    public class SHA256CngService : IHashService
    {
        public byte[] GetHash(byte[] sourceBuffer)
        {
            try
            {
                var sha256Cng = SHA256Cng.Create();
                return sha256Cng.ComputeHash(sourceBuffer);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"SHA256Cng service GetSHA256CngHash error: {e.Message}");
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
