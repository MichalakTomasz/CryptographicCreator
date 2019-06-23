namespace Commons
{
    public interface IAESSerializationService
    {
        byte[] DeserializeEncryptedData(string path);
        AESKey DeserializeKey(string path);
        void SerializeEncryptedData(byte[] buffer, string path);
        void SerializeKey(AESKey aesKey, string path);
    }
}