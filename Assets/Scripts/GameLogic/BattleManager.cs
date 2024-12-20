using Core;
using Data;
using Data.ScriptableObjects;
using UnityEngine;

namespace GameLogic
{
    public class BattleManager
    {
        private HeroData[] heroes;
        private EnemyData enemy;
        private int level;

        public void Initialize(GameState gameState, EntityService entityService)
        {
            level = gameState.CurrentLevel;
            enemy = entityService.GetEnemyByIndex(level);
            // Instantiate and initialize enemy GameObject here
            heroes = new HeroData[3];

            for (var i = 0; i < Constants.MaxSelectedHeroes; i++)
            {
                var heroIndex = gameState.SelectedHeroIndexes[i];
                var hero = entityService.GetHeroByIndex(heroIndex);
                Debug.Log($"Hero {i+1}: {hero.EntityName}, HP: {hero.Health}, AP: {hero.AttackPower}");
                heroes[i] = hero;
                // Instantiate and initialize hero GameObjects here
            }
        }
    }
}