using Data.ScriptableObjects;
using UnityEngine;

namespace GameLogic.Battle
{
    /// <summary>
    /// Manages enemy-controlled units (currently a single enemy).
    /// </summary>
    public class OpponentManager
    {
        private readonly EnemyData _enemy;

        public OpponentManager(EnemyData enemy)
        {
            _enemy = enemy;
        }

        public EnemyData GetEnemy() => _enemy;

        public void ApplyDamage(float damage)
        {
            //_enemy.Health -= damage;
            Debug.Log($"{_enemy.EntityName} took {damage} damage!");
        }
    }
}