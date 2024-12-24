using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Progression;
using Data;

namespace GameLogic
{
    /// <summary>
    /// Manipulates UserData and saves/loads via Progression Service
    /// </summary>
    public class UserDataManager
    {
        private readonly ProgressionService _progressionService;
        private readonly UserData _userData;
        //private readonly UserDataUI _userDataUI;
        
        private ProgressionData progressionData;

        public UserDataManager(UserData userData, ProgressionService progressionService)
        {
            _userData = userData;
            //_userDataUI = userDataUI;
            _progressionService = progressionService;
        }
        
        public int CurrentLevel => _userData.CurrentLevel;
        public List<int> SelectedHeroIndexes => _userData.SelectedHeroIndexes;

        public async Task InitializeUserDataAsync()
        {
            progressionData = await _progressionService.LoadProgressionAsync();
            _userData.CurrentLevel = progressionData.CurrentLevel;
            _userData.SelectedHeroIndexes = progressionData.SelectedHeroIndexes;
        }

        public void UpdateLevel(int level)
        {
            _userData.CurrentLevel = level;
            SaveUserDataAsync();
        }

        public void UpdateSelectedHeroes(List<int> heroIndexes)
        {
            _userData.SelectedHeroIndexes = heroIndexes;
            SaveUserDataAsync();
        }

        private async Task SaveUserDataAsync()
        {
            progressionData.CurrentLevel = _userData.CurrentLevel;
            progressionData.SelectedHeroIndexes = _userData.SelectedHeroIndexes;
            await _progressionService.SaveProgressionAsync(progressionData);
        }
        
        // Extracts UserData for DI
        public UserData GetUserDataUI()
        {
            return _userData;
        }
    }
}