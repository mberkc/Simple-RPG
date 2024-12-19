using Core.Progression;

namespace Core.Initializable
{
    public interface IInitializable
    {
        public void Initialize(ProgressionService progressionService);
        public void Cleanup();
    }
}