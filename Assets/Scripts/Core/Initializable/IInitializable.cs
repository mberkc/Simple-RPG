namespace Core.Initializable
{
    public interface IInitializable
    {
        public void Initialize();
        public void Cleanup();
    }
}