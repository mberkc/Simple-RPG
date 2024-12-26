using System.Collections.Generic;
using Core;

namespace GameLogic.Battle
{
    public class HeroProgressionService
    {
        private readonly UserDataManager _userDataManager;

        public HeroProgressionService(UserDataManager userDataManager)
        {
            _userDataManager = userDataManager;
        }
        
        /// <summary>
        /// Handle Hero Progression
        /// </summary>
        internal void CheckHeroProgression(bool victory, List<int> aliveHeroIndexes, int playAmount)
        {
            if (victory && aliveHeroIndexes != null)
                _userDataManager.UpdateHeroExperience(aliveHeroIndexes, Constants.ExperiencePerBattle);
            
            if (playAmount % Constants.UnlockThreshold == 0)
                _userDataManager.UnlockRandomHero();
        }
    }
}