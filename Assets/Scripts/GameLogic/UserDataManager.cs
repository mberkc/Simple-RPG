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
        //private readonly UserDataVisual _userDataVisual;
        
        private ProgressionData progressionData;

        public UserDataManager(UserData userData, ProgressionService progressionService)
        {
            _userData = userData;
            //_userDataVisual = _userDataVisual;
            _progressionService = progressionService;
        }
        
        public int BattlePlayAmount => _userData.BattlePlayAmount;
        public int CurrentLevel => _userData.CurrentLevel;
        public List<int> SelectedHeroIndexes => _userData.SelectedHeroIndexes;

        public async Task InitializeUserDataAsync()
        {
            progressionData = await _progressionService.LoadProgressionAsync();
            _userData.BattlePlayAmount = progressionData.BattlePlayAmount;
            _userData.CurrentLevel = progressionData.CurrentLevel;
            _userData.SelectedHeroIndexes = progressionData.SelectedHeroIndexes;
        }

        public void UpdateLevelAndPlayAmount(int level, int playAmount)
        {
            _userData.CurrentLevel = level;
            _userData.BattlePlayAmount = playAmount;
            SaveUserDataAsync();
        }

        public void UpdateSelectedHeroes(List<int> heroIndexes)
        {
            _userData.SelectedHeroIndexes = heroIndexes;
            SaveUserDataAsync();
        }

        private async Task SaveUserDataAsync()
        {
            progressionData.BattlePlayAmount = _userData.BattlePlayAmount;
            progressionData.CurrentLevel = _userData.CurrentLevel;
            progressionData.SelectedHeroIndexes = _userData.SelectedHeroIndexes;
            await _progressionService.SaveProgressionAsync(progressionData);
        }
        
        public UserData GetUserData()
        {
            return _userData;
        }
        
        // Extracts UserData for DI (Visual Assembly)
        public UserData GetUserDataVisual()
        {
            // _userDataVisual
            return _userData;
        }
    }
}