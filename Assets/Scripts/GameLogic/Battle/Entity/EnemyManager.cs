using Data.ScriptableObjects;
using UnityEngine;

namespace GameLogic.Battle.Entity
{
    /// <summary>
    /// Obsolete Class
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        private EnemyData enemyData;

        public void Initialize(EnemyData enemy)
        {
            enemyData = enemy;
            Debug.Log($"Enemy Initialized: {enemyData.EntityName}, HP: {enemyData.Health}, AP: {enemyData.AttackPower}");
        }
    }
}