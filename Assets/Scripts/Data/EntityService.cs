using System.Linq;
using Data.ScriptableObjects;

namespace Data
{
    public class EntityService
    {
        private readonly HeroData[] _heroes;
        private readonly EnemyData[] _enemies;

        public EntityService(HeroData[] heroes, EnemyData[] enemies)
        {
            _heroes = heroes.OrderBy(h => h.Index).ToArray();
            _enemies = enemies.OrderBy(e => e.Index).ToArray();
        }

        public HeroData GetHeroByIndex(int index) => _heroes.ElementAtOrDefault(index);
        public EnemyData GetEnemyByIndex(int index) => _enemies.ElementAtOrDefault(index);
    }
}