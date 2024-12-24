using System.Threading.Tasks;
using Core.Progression.ProgressionStorage;

namespace Core.Progression
{
    public class ProgressionService
    {
        private readonly IProgressionStorage _storage;
        
        public ProgressionService(IProgressionStorage storage)
        {
            _storage = storage;
        }

        public async Task<ProgressionData> LoadProgressionAsync()
        {
            return await _storage.LoadAsync();
        }

        public async Task SaveProgressionAsync(ProgressionData progressionData)
        {
            await _storage.SaveAsync(progressionData);
        }
    }
}