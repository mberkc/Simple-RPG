namespace Core.Encryption
{
    public interface IEncryptionService
    {
        byte[] EncryptData(string data);
        string DecryptData(byte[] encryptedData);
    }
}