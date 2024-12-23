using System.Threading.Tasks;
using Data;
using GameLogic.Battle.BotStrategy;
using GameLogic.Battle.Entity;
using UnityEngine;

namespace GameLogic.Battle
{
    /// <summary>
    /// Manages enemy-controlled units (currently a single enemy).
    /// </summary>
    public class OpponentManager
    {
        private readonly IBotStrategy _botStrategy;
        private readonly BattleEntity _enemyEntity;

        public OpponentManager(BattleEntityFactory entityFactory, GameState gameState, EntityService entityService, IBotStrategy botStrategy)
        { 
            _botStrategy = botStrategy;
            var enemy = entityService.GetEnemyByIndex(gameState.CurrentLevel); 
            _enemyEntity = entityFactory.CreateEnemy(enemy, GetEnemySpawnPosition());
        }
        
        public async Task HandleOpponentAttack(BattleEntity[] availableTargets, CombatSystem combatSystem)
        {
            var target = await _botStrategy.ChooseTarget(availableTargets);
            if (target != null)
                combatSystem.ExecuteAttack(_enemyEntity, target);
            else
                Debug.LogWarning("No valid target available for opponent.");
        }
        
        public bool CheckIfEnemyIsDefeated => !_enemyEntity.IsAlive;
        
        private Vector3 GetEnemySpawnPosition()
        {
            return new Vector3(0, 0, 0);
        }
    }
}