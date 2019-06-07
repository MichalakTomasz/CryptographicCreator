using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Commons.Services
{
    public class SHA256Service : IHashService
    {
        public byte[] GetHash(byte[] sourceBuffer)
        {
            try
            {
                var sha256 = SHA256.Create();
                return sha256.ComputeHash(sourceBuffer);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"SHA256 service GetSHA256Hash error: {e.Message}");
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
