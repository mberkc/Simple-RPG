using System.Collections.Generic;
using System.Linq;
using Core;
using Data;
using GameLogic.Battle.Entity;
using UnityEngine;

namespace GameLogic.Battle
{
    /// <summary>
    /// Manages player-controlled heroes via UI input(s).
    /// </summary>
    public class PlayerManager
    {
        private readonly BattleEntity[] _heroEntities;

        public PlayerManager(BattleEntityFactory entityFactory, GameState gameState, EntityService entityService)
        {
            _heroEntities = new BattleEntity[Constants.MaxSelectedHeroes];

            for (var i = 0; i < Constants.MaxSelectedHeroes; i++)
            {
                var heroIndex = gameState.SelectedHeroIndexes[i];
                var hero = entityService.GetHeroByIndex(heroIndex);
                _heroEntities[i] = entityFactory.CreateHero(hero, GetHeroSpawnPosition(i));
            }
        }
        
        public void HandlePlayerAttack(int attackerIndex, BattleEntity target, CombatSystem combatSystem)
        {
            var hero = _heroEntities[attackerIndex];
            combatSystem.ExecuteAttack(hero, target);
        }
        
        public BattleEntity[] GetHeroEntities() => _heroEntities;
        
        public bool CheckIfAllHeroesAreDefeated => _heroEntities.All(hero => !hero.IsAlive);

        public List<int> GetAliveHeroIndexes()
        {
            return _heroEntities.Where(hero => hero.IsAlive).Select(hero => hero.Index).ToList();
        }
        
        private Vector3 GetHeroSpawnPosition(int index)
        {
            // index based spawn position
            return new Vector3(-2 + index * 2, 0, 0);
        }
    }
}