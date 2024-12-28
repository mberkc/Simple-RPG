using System.Collections.Generic;
using System.Linq;
using Data.ScriptableObjects;

namespace Data
{
    public class EnemyService
    {
        private readonly EnemySO[] _enemies;
        
        public EnemySO GetEnemyByLevel(int level) => _enemies.ElementAtOrDefault(level-1);
        //public EnemySO GetEnemyByIndex(int index) => _enemies.ElementAtOrDefault(index);

        public EnemyService(EnemySO[] enemies, out int availableEnemies)
        {
            _enemies = RemoveEmptyEnemiesAndSort(enemies);
            availableEnemies = _enemies.Length;
        }
        
        private EnemySO[] RemoveEmptyEnemiesAndSort(EnemySO[] enemies)
        {
            var amount = enemies.Length;
            var enemyList = new List<EnemySO>();
            for (var i = 0; i < amount; i++)
            {
                var enemy = enemies[i];
                if(enemy == null) continue;
                enemyList.Add(enemy);
            }

            return enemyList.OrderBy(e => e.Index).ToArray();
        }
    }
}