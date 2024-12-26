using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Progression;
using Data;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// Manipulates UserData and saves/loads via Progression Service
    /// </summary>
    public class UserDataManager
    {
        private readonly ProgressionService _progressionService;
        private readonly UserData _userData;
        
        private ProgressionData progressionData;

        public UserDataManager(UserData userData, ProgressionService progressionService)
        {
            _userData = userData;
            _progressionService = progressionService;
        }
        
        
        public List<int> SelectedHeroIndexes => _userData.SelectedHeroIndexes;
        public int CurrentLevel => _userData.CurrentLevel;
        public int BattlePlayAmount => _userData.BattlePlayAmount;
        public HeroCollection HeroCollection => _userData.HeroCollection;
        
        public async Task InitializeUserDataAsync()
        {
            progressionData = await _progressionService.LoadProgressionAsync();
            GetProgressionData();
        }

        // TODO: change only necessary parts!
        private async Task SaveUserDataAsync(bool updateEverything = true)
        {
            if (updateEverything)
                SetProgressionData();
            await _progressionService.SaveProgressionAsync(progressionData);
        }

        private void GetProgressionData()
        {
            _userData.BattlePlayAmount = progressionData.BattlePlayAmount;
            _userData.CurrentLevel = progressionData.CurrentLevel;
            _userData.SelectedHeroIndexes = progressionData.SelectedHeroIndexes;
            for (var i = 0; i < Constants.TotalHeroes; i++)
                _userData.HeroCollection.Heroes[i].UserHeroData = progressionData.SerializableUserHeroCollection.SerializableUserHeroes[i].UserHeroData;

        }
        
        private void SetProgressionData()
        {
            progressionData.BattlePlayAmount = _userData.BattlePlayAmount;
            progressionData.CurrentLevel = _userData.CurrentLevel;
            progressionData.SelectedHeroIndexes = _userData.SelectedHeroIndexes;
            for (var i = 0; i < Constants.TotalHeroes; i++)
                progressionData.SerializableUserHeroCollection.SerializableUserHeroes[i].UserHeroData = _userData.HeroCollection.Heroes[i].UserHeroData;
        }

        public void UpdateLevelAndPlayAmount(int level, int playAmount)
        {
            _userData.CurrentLevel = level;
            _userData.BattlePlayAmount = playAmount;
            progressionData.BattlePlayAmount = _userData.BattlePlayAmount;
            progressionData.CurrentLevel = _userData.CurrentLevel;
            SaveUserDataAsync(false);
        }

        public void UpdateSelectedHeroes(List<int> heroIndexes)
        {
            _userData.SelectedHeroIndexes = heroIndexes;
            progressionData.SelectedHeroIndexes = _userData.SelectedHeroIndexes;
            SaveUserDataAsync(false);
        }
        
        public void UpdateHeroExperience(List<int> heroIndexes, int experienceGain)
        {
            foreach (var index in heroIndexes)
            {
                var heroData = HeroCollection.Heroes[index].UserHeroData;
                heroData.Experience += experienceGain;
                if (heroData.Experience < Constants.HeroLevelUpExperienceThreshold) continue;
                
                heroData.Level++;
                heroData.Experience -= Constants.HeroLevelUpExperienceThreshold;
            }
            SaveUserDataAsync(false);
        }
        
        public void UnlockRandomHero()
        {
            var lockedHeroes = new List<HeroData>();
            foreach (var hero in HeroCollection.Heroes)
            {
                if (!hero.UserHeroData.Unlocked)
                    lockedHeroes.Add(hero);
            }

            if (lockedHeroes.Count == 0)
            {
                Debug.Log("All heroes are already unlocked!");
                return;
            }

            var randomIndex = Random.Range(0, lockedHeroes.Count);
            lockedHeroes[randomIndex].UserHeroData.Unlocked = true;
            Debug.Log($"Hero {lockedHeroes[randomIndex].EntityName} unlocked!");
            SaveUserDataAsync(false);
        }
        
        public void UpdateHeroProgress()
        {
            SaveUserDataAsync();
        }
        
        public UserData GetUserData()
        {
            return _userData;
        }
        
        // Extracts UserData for DI (Visual Assembly)
        public UserData GetUserDataVisual()
        {
            return _userData;
        }
    }
}