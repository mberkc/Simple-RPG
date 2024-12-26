using System.Threading.Tasks;
using Data;
using GameLogic.Battle.BotStrategy;
using GameLogic.Battle.Entity;

namespace GameLogic.Battle
{
    /// <summary>
    /// Manages enemy-controlled units (currently a single enemy) & chooses target via Bot Strategy
    /// </summary>
    public class OpponentManager
    {
        private readonly IBotStrategy _botStrategy;
        private readonly BattleEntity _enemyEntity;

        public OpponentManager(UserData userData, EnemyService enemyService, EntitySpawner entitySpawner, IBotStrategy botStrategy)
        { 
            _botStrategy = botStrategy;
            var enemy = enemyService.GetEnemyByIndex(userData.CurrentLevel); 
            _enemyEntity = entitySpawner.SpawnEnemy(enemy);
        }
        
        public async Task<BattleEntity> GetTarget(BattleEntity[] availableTargets)
        {
            return await _botStrategy.ChooseTarget(availableTargets);
        }
        
        public BattleEntity GetEnemyEntity => _enemyEntity;
        
        public bool CheckIfEnemyIsDefeated => !_enemyEntity.IsAlive;
    }
}