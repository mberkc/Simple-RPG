using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core.Encryption
{
    public class AESEncryptionService : IEncryptionService
    {
        private readonly byte[] _key;
        
        public AESEncryptionService(string key)
        {
            if (key.Length != 16) // AES requires a 16-byte key
                throw new ArgumentException("Key must be 16 characters long for AES encryption.");
            _key = Encoding.UTF8.GetBytes(key);
        }
        
        public byte[] EncryptData(string data)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();
            
            var plainBytes = Encoding.UTF8.GetBytes(data);
            
            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);
            
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            cs.Write(plainBytes, 0, plainBytes.Length);
            cs.FlushFinalBlock();
            
            return ms.ToArray();
        }

        public string DecryptData(byte[] encryptedData)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            
            var iv = new byte[aes.BlockSize / 8];
            Array.Copy(encryptedData, 0, iv, 0, iv.Length);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(encryptedData, iv.Length, encryptedData.Length - iv.Length);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}