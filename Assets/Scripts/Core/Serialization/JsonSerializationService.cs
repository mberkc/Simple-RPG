using UnityEngine;

namespace Core.Serialization
{
    public class JsonSerializationService : ISerializationService
    {
        public string Serialize<T>(T data)
        {
            return JsonUtility.ToJson(data, true);
        }

        public T Deserialize<T>(string data)
        {
            return JsonUtility.FromJson<T>(data);
        }
    }
}