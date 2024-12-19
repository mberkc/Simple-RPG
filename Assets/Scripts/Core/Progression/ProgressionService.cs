using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Progression.ProgressionStorage;
using UnityEngine;

namespace Core.Progression
{
    public class ProgressionService
    {
        private readonly IProgressionStorage _storage;
        private ProgressionData currentProgression = new ProgressionData();
        
        public ProgressionService(IProgressionStorage storage)
        {
            _storage = storage;
        }

        public async Task LoadProgressionAsync()
        {
            currentProgression = await _storage.LoadAsync();
            Debug.Log("Progression data loaded.");
        }

        public async Task SaveCurrentLevelAsync(int level)
        {
            currentProgression.CurrentLevel = level;
            await _storage.SaveAsync(currentProgression);
            Debug.Log($"Current level set to {level}.");
        }

        public async Task SaveSelectedHeroesAsync(List<int> heroIndexes)
        {
            currentProgression.SelectedHeroIndexes = heroIndexes;
            await _storage.SaveAsync(currentProgression);
            Debug.Log("Selected heroes updated.");
        }
        
        public void SaveCurrentLevel(int level)
        {
            currentProgression.CurrentLevel = level;
            _storage.SaveAsync(currentProgression);
        }

        public void SaveSelectedHeroes(List<int> heroIndexes)
        {
            currentProgression.SelectedHeroIndexes = heroIndexes;
            _storage.SaveAsync(currentProgression);
        }

        public int GetCurrentLevel()
        {
            return currentProgression.CurrentLevel;
        }
        
        public List<int> GetSelectedHeroes()
        {
            return currentProgression.SelectedHeroIndexes;
        }
    }
}