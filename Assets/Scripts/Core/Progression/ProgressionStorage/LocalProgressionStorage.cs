using System;
using System.IO;
using System.Threading.Tasks;
using Core.Encryption;
using Core.Serialization;
using UnityEngine;

namespace Core.Progression.ProgressionStorage
{
    public class LocalProgressionStorage : IProgressionStorage
    {
        private readonly string _savePath = Path.Combine(Application.persistentDataPath, "progression.dat");
        private readonly IEncryptionService _encryptionService;
        private readonly ISerializationService _serializationService;
        
        public LocalProgressionStorage(IEncryptionService encryptionService, ISerializationService serializationService)
        {
            _encryptionService = encryptionService;
            _serializationService = serializationService;
        }

        public async Task<ProgressionData> LoadAsync()
        {
            if (!File.Exists(_savePath))
            {
                Debug.Log("No progression file found. Initializing new progression.");
                return new ProgressionData();
            }
            try
            {
                var encryptedData = await File.ReadAllBytesAsync(_savePath);
                var serializedData = _encryptionService.DecryptData(encryptedData);
                return _serializationService.Deserialize<ProgressionData>(serializedData);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load progression data: {ex.Message}");
                return new ProgressionData();
            }
        }

        public async Task SaveAsync(ProgressionData progressionData)
        {
            try
            {
                var serializedData = _serializationService.Serialize(progressionData);
                var encryptedData = _encryptionService.EncryptData(serializedData);
                await File.WriteAllBytesAsync(_savePath, encryptedData);
                Debug.Log("Progression saved locally.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save progression data: {ex.Message}");
            }
        }
    }
}