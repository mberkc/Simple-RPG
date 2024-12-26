using System.Linq;
using Data.ScriptableObjects;

namespace Data
{
    public class EnemyService
    {
        private readonly EnemySO[] _enemies;

        public EnemyService(EnemySO[] enemies)
        {
            _enemies = enemies.OrderBy(e => e.Index).ToArray();
        }

        public EnemySO GetEnemyByIndex(int index) => _enemies.ElementAtOrDefault(index);
        public EnemySO GetEnemyByLevel(int index) => _enemies.ElementAtOrDefault(index);
    }
}