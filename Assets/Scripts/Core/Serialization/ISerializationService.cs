namespace Core.Serialization
{
    public interface ISerializationService
    {
        string Serialize<T>(T data);
        T Deserialize<T>(string data);
    }
}