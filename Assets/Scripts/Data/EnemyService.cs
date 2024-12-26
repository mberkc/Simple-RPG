using System.Collections.Generic;
using System.Linq;
using Data.ScriptableObjects;

namespace Data
{
    public class EnemyService
    {
        private readonly EnemySO[] _enemies;

        public EnemyService(List<EnemySO> enemies)
        {
            _enemies = enemies.OrderBy(e => e.Index).ToArray();
        }

        public EnemySO GetEnemyByLevel(int level) => _enemies.ElementAtOrDefault(level-1);
        public EnemySO GetEnemyByIndex(int index) => _enemies.ElementAtOrDefault(index);
    }
}