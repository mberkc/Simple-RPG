using System.Threading.Tasks;

namespace Core.Progression.ProgressionStorage
{
    public interface IProgressionStorage
    {
        Task<ProgressionData> LoadAsync();
        Task SaveAsync(ProgressionData progressionData);
    }
}