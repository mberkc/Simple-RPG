using Data;
using Data.ScriptableObjects;

namespace GameLogic.Battle.Entity
{
    public class BattleEntitySpawner
    {
        private readonly BattleEntityFactory _entityFactory;

        public BattleEntitySpawner(BattleEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public BattleEntity SpawnHero(HeroData heroData, int boardIndex)
        {
            return _entityFactory.CreateHero(heroData, boardIndex);
        }

        public BattleEntity SpawnEnemy(EnemySO enemySo, int boardIndex)
        {
            return _entityFactory.CreateEnemy(enemySo, boardIndex);
        }
    }
}