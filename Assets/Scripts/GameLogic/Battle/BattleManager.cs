using Core;
using Data;
using Data.ScriptableObjects;
using UnityEngine;

namespace GameLogic.Battle
{
    public class BattleManager
    {
        private readonly PlayerManager _playerManager;
        private readonly OpponentManager _opponentManager;
        
        private readonly HeroData[] _heroes;
        private readonly EnemyData _enemy;
        private readonly int _level;
        
        // Command: For battle actions (e.g., attacks).
        // Factory: To create and initialize hero/enemy instances dynamically
        // State: Manage battle states (e.g., Idle, Combat(PlayerTurn, OpponentTurn), Victory, Defeat, etc...).
        
        public BattleManager(GameState gameState, EntityService entityService)
        {
            // Instantiate and initialize enemy GameObject here
            _level = gameState.CurrentLevel;
            _enemy = entityService.GetEnemyByLevel(_level);
            _heroes = new HeroData[3];

            for (var i = 0; i < Constants.MaxSelectedHeroes; i++)
            {
                var heroIndex = gameState.SelectedHeroIndexes[i];
                var hero = entityService.GetHeroByIndex(heroIndex);
                Debug.Log($"Hero {i+1}: {hero.EntityName}, HP: {hero.Health}, AP: {hero.AttackPower}");
                _heroes[i] = hero;
                // Instantiate and initialize hero GameObjects here
            }
            
            _playerManager = new PlayerManager(_heroes);
            _opponentManager = new OpponentManager(_enemy);
        }
    }
}