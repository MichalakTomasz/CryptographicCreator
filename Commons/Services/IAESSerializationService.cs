namespace Commons
{
    public interface IAESSerializationService
    {
        byte[] Deserialize(string path);
        AESKey DeserializeKey(string path);
        void Serialize(byte[] buffer, string path);
        void SerializeKey(AESKey aesKey, string path);
    }
}