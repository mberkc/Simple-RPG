using System.Threading.Tasks;

namespace Core.Progression.ProgressionStorage
{
    public class CloudProgressionStorage : IProgressionStorage
    {
        public Task<ProgressionData> LoadAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync(ProgressionData progressionData)
        {
            throw new System.NotImplementedException();
        }
    }
}