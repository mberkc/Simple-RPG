namespace Core.BootStrapper
{
    public interface IBootStrapper
    {
        public bool IsEnabled { get; }
        public void Initialize();
    }
}