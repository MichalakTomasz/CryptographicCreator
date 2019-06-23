namespace Commons
{
    public interface IAESMaskService
    {
        byte[] Mask(AESKey aesKey);
        AESKey Unmask(byte[] buffer);
    }
}