using System.Threading.Tasks;
using Core;
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

        public OpponentManager(UserData userData, EnemyService enemyService, BattleEntitySpawner battleEntitySpawner, IBotStrategy botStrategy)
        { 
            _botStrategy = botStrategy;
            var enemy = enemyService.GetEnemyByLevel(userData.CurrentLevel); 
            _enemyEntity = battleEntitySpawner.SpawnEnemy(enemy, Constants.EnemyBoardIndex);
        }
        
        public async Task<BattleEntity> GetTarget(BattleEntity[] availableTargets)
        {
            return await _botStrategy.ChooseTarget(availableTargets);
        }
        
        public BattleEntity GetEnemyEntity => _enemyEntity;
        
        public bool CheckIfEnemyIsDefeated => !_enemyEntity.IsAlive;
    }
}