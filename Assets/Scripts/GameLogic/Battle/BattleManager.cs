using Core;
using Data;
using Data.ScriptableObjects;
using UnityEngine;

namespace GameLogic.Battle
{
    public class BattleManager
    {
        private PlayerManager _playerManager;
        private OpponentManager _opponentManager;
        
        private HeroData[] heroes;
        private EnemyData enemy;
        private int level;
        
        // Command: For battle actions (e.g., attacks).
        // Factory: To create and initialize hero/enemy instances dynamically
        // State: Manage battle states (e.g., Idle, Combat(PlayerTurn, OpponentTurn), Victory, Defeat, etc...).

        public void Initialize(GameState gameState, EntityService entityService)
        {
            
            // Instantiate and initialize enemy GameObject here
            level = gameState.CurrentLevel;
            enemy = entityService.GetEnemyByLevel(level);
            heroes = new HeroData[3];

            for (var i = 0; i < Constants.MaxSelectedHeroes; i++)
            {
                var heroIndex = gameState.SelectedHeroIndexes[i];
                var hero = entityService.GetHeroByIndex(heroIndex);
                Debug.Log($"Hero {i+1}: {hero.EntityName}, HP: {hero.Health}, AP: {hero.AttackPower}");
                heroes[i] = hero;
                // Instantiate and initialize hero GameObjects here
            }
            
            // Inject heroes & enemies?
            _playerManager = new PlayerManager(gameState, entityService);
            _opponentManager = new OpponentManager(gameState, entityService);
        }
    }
}