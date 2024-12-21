namespace GameStartupSystem.Bootstrapper
{
    public interface IBootstrapper
    {
        public bool IsEnabled { get; }
        public void Initialize();
        public void Awake();
    }
}