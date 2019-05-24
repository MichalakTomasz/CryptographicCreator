namespace Commons
{
    public interface ICompressionService
    {
        bool Compress(byte[] source, string path);
        byte[] Decompress(string path);
    }
}