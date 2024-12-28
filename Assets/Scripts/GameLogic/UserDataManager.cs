using System.Collections.Generic;
using System.Linq;
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
        private readonly int _maxLevel;
        
        private ProgressionData progressionData;
        
        /// <summary>
        /// UserData for DI (GameLogic Assembly)
        /// </summary>
        public UserData GetUserData => _userData;
        
        /// <summary>
        /// UserData for DI (Visual Assembly)
        /// </summary>
        public UserData GetUserDataVisual => _userData; // Userdata needed by visual assembly might differ in the future, so we have this property

        public UserDataManager(UserData userData, ProgressionService progressionService, int maxLevel)
        {
            _userData = userData;
            _progressionService = progressionService;
            _maxLevel = maxLevel;
        }
        
        public List<int> SelectedHeroIndexes => _userData.SelectedHeroIndexes;
        public int CurrentLevel => _userData.CurrentLevel;
        public int BattlePlayAmount => _userData.BattlePlayAmount;
        
        /// <summary>
        /// Initializes userdata from progressiondata
        /// </summary>
        public async Task InitializeUserDataAsync()
        {
            progressionData = await _progressionService.LoadProgressionAsync();
            MapProgressionDataToUserData();
        }
        
        /// <summary>
        /// Saves userdata as progressiondata
        /// </summary>
        private async Task SaveUserDataAsync()
        {
            await _progressionService.SaveProgressionAsync(progressionData);
        }
        
        public async Task UpdateSelectedHeroes(List<int> heroIndexes)
        {
            _userData.SelectedHeroIndexes = heroIndexes;
            progressionData.SelectedHeroIndexes = _userData.SelectedHeroIndexes;
            await SaveUserDataAsync();
        }
        
        public async Task SaveAllChangesAsync()
        { 
            MapUserDataToProgressionData(); // This makes sure all changes are updated for progresiondata (if we need to optimize, just update required ones)
            await SaveUserDataAsync();
        }
        
        #region UserData <=> ProgressionData Mapping

        private void MapProgressionDataToUserData()
        {
            if(progressionData.CurrentLevel > _maxLevel) progressionData.CurrentLevel = _maxLevel;
            
            _userData.BattlePlayAmount = progressionData.BattlePlayAmount;
            _userData.CurrentLevel = progressionData.CurrentLevel;
            _userData.SelectedHeroIndexes = progressionData.SelectedHeroIndexes;
            
            for (var i = 0; i < Constants.TotalHeroes; i++)
            {
                var serializedHero = progressionData.SerializableUserHeroCollection.SerializableUserHeroes[i];
                var heroData = _userData.GetHeroData(i);
                heroData.UserHeroData = serializedHero.UserHeroData;
                UpdateModifiedStats(heroData);
            }
        }
        
        private void MapUserDataToProgressionData()
        {
            progressionData.BattlePlayAmount = _userData.BattlePlayAmount;
            progressionData.CurrentLevel = _userData.CurrentLevel;
            progressionData.SelectedHeroIndexes = _userData.SelectedHeroIndexes;
            
            for (var i = 0; i < Constants.TotalHeroes; i++)
                progressionData.SerializableUserHeroCollection.SerializableUserHeroes[i].UserHeroData = _userData.GetUserHeroData(i);
        }

        #endregion

        #region Save After Battle End

        public void UpdateLevelAndPlayAmount(int level, int playAmount)
        {
            _userData.CurrentLevel = level;
            _userData.BattlePlayAmount = playAmount;
        }
        
        public void UnlockRandomHero()
        {
            var lockedHeroes = _userData.HeroCollection.Heroes
                .Where(hero => !hero.UserHeroData.Unlocked)
                .ToList();

            if (!lockedHeroes.Any()) return;

            var randomHero = lockedHeroes[Random.Range(0, lockedHeroes.Count)];
            randomHero.UserHeroData.Unlocked = true;
        }
        
        public void UpdateHeroExperience(List<int> heroIndexes, int experienceGain)
        {
            foreach (var index in heroIndexes)
            {
                var heroData = _userData.GetHeroData(index);
                var userHeroData = heroData.UserHeroData;
                userHeroData.Experience += experienceGain;
                if (userHeroData.Experience < Constants.HeroLevelUpExperienceThreshold) continue;
                
                ++userHeroData.Level;
                userHeroData.Experience -= Constants.HeroLevelUpExperienceThreshold;
                UpdateModifiedStats(heroData);
            }
        }
        
        private void UpdateModifiedStats(HeroData heroData)
        {
            if(heroData.UserHeroData.Level == 1) return;
            
            heroData.ModifiedHealth = CoreUtility.CalculateModifiedAttribute(heroData.BaseHealth, Constants.HeroLevelUpHealthModifier, heroData.UserHeroData.Level);
            heroData.ModifiedAttackPower = CoreUtility.CalculateModifiedAttribute(heroData.BaseAttackPower, Constants.HeroLevelUpAttackPowerModifier, heroData.UserHeroData.Level);
        }

        #endregion
    }
}