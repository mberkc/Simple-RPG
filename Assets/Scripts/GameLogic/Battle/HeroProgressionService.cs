using System.Collections.Generic;
using Core;
using Data;
using Data.ScriptableObjects;
using UnityEngine;

namespace GameLogic.Battle
{
    public class HeroProgressionService
    {
        private readonly UserDataManager _userDataManager;
        private readonly EntityService _entityService;

        public HeroProgressionService(UserDataManager userDataManager, EntityService entityService)
        {
            _userDataManager = userDataManager;
            _entityService = entityService;
        }
        
        /// <summary>
        /// Handle Hero Progression
        /// </summary>
        internal void CheckHeroProgression(bool victory, List<int> aliveHeroIndexes, int playAmount)
        {
            if (victory)
                CheckExperienceGain(aliveHeroIndexes);

            CheckHeroUnlock(playAmount);
        }
        

        /// <summary>
        /// Handle experience gain for surviving heroes.
        /// </summary>
        private void CheckExperienceGain(List<int> aliveHeroIndexes)
        {
            if(aliveHeroIndexes == null || aliveHeroIndexes.Count == 0) return;
            var count = aliveHeroIndexes.Count;
            
            for (var i = 0; i < count; i++)
            {
                var heroIndex = aliveHeroIndexes[i];
                var heroData = _entityService.GetHeroByIndex(heroIndex);
                if (heroData == null) continue;
                
                heroData.GainExperience(1);
                Debug.Log($"Hero {heroData.EntityName} gained experience. Current Level: {heroData.Level}, Experience: {heroData.Experience}");
            }
        }

        /// <summary>
        /// Unlock a random locked hero.
        /// </summary>
        private void CheckHeroUnlock(int battlePlayAmount)
        {
            if (battlePlayAmount % Constants.UnlockThreshold != 0) return;
            
            // If all heroes are unlocked => return 
            // Unlock Random Hero (locked one)
            
            var lockedHeroes = new List<HeroData>();
            for (var i = 0; i < Constants.TotalHeroes; i++)
            {
                var heroData = _entityService.GetHeroByIndex(i);
                lockedHeroes.Add(heroData);
            }

            if (lockedHeroes.Count == 0)
            {
                Debug.Log("All heroes are already unlocked!");
                return;
            }

            var randomHeroIndex = Random.Range(0, lockedHeroes.Count);
            var unlockedHero = lockedHeroes[randomHeroIndex];

            unlockedHero.Unlock();
            Debug.Log($"Hero {unlockedHero.EntityName} has been unlocked!");
        }
    }
}