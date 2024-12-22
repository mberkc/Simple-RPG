using Core;
using Data;
using Data.ScriptableObjects;
using UnityEngine;

namespace GameLogic.Battle
{
    /// <summary>
    /// Manages player-controlled heroes.
    /// </summary>
    public class PlayerManager
    {
        private readonly HeroData[] _heroes;

        public PlayerManager(GameState gameState, EntityService entityService)
        {
            _heroes = new HeroData[Constants.MaxSelectedHeroes];
            for (var i = 0; i < gameState.SelectedHeroIndexes.Count; i++)
            {
                var heroIndex = gameState.SelectedHeroIndexes[i];
                _heroes[i] = entityService.GetHeroByIndex(heroIndex);
            }
        }

        public HeroData[] GetHeroes() => _heroes;

        public void ApplyDamage(int heroIndex, float damage)
        {
            var hero = _heroes[heroIndex];
            //hero.Health -= damage;
            Debug.Log($"{hero.EntityName} took {damage} damage!");
        }
    }
}