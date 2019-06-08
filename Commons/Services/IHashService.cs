namespace Commons
{
    public interface IHashService
    {
        byte[] GetHash(byte[] sourceBuffer);
        bool VerifyHash(byte[] sourceBuffer, byte[] hash);
    }
}