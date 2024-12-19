namespace Core.Encryption
{
    public class NoEncryptionService : IEncryptionService
    {
        public byte[] EncryptData(string data)
        {
            return System.Text.Encoding.UTF8.GetBytes(data);
        }

        public string DecryptData(byte[] encryptedData)
        {
            return System.Text.Encoding.UTF8.GetString(encryptedData);
        }
    }
}