namespace Commons
{
    public interface IGZipCompressionService
    {
        byte[] Compress(byte[] source);
        byte[] Decompress(byte[] compressedData);
    }
}