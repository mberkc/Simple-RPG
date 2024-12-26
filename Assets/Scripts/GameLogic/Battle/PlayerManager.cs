using System.Collections.Generic;
using System.Linq;
using Core;
using Data;
using GameLogic.Battle.Entity;

namespace GameLogic.Battle
{
    /// <summary>
    /// Manages player-controlled heroes
    /// </summary>
    public class PlayerManager
    {
        private readonly BattleEntity[] _heroEntities;

        public PlayerManager(UserData userData, EntitySpawner entitySpawner)
        {
            _heroEntities = new BattleEntity[Constants.MaxSelectedHeroes];
            for (var i = 0; i < Constants.MaxSelectedHeroes; i++)
            {
                var heroIndex = userData.SelectedHeroIndexes[i];
                var hero = userData.GetHeroData(heroIndex);
                _heroEntities[i] = entitySpawner.SpawnHero(hero, i);
            }
        }
        
        public BattleEntity[] GetHeroEntities => _heroEntities;
        
        public bool CheckIfAllHeroesAreDefeated => _heroEntities.All(hero => !hero.IsAlive);

        public List<int> GetAliveHeroIndexes => _heroEntities.Where(hero => hero.IsAlive).Select(hero => hero.Index).ToList();
    }
}