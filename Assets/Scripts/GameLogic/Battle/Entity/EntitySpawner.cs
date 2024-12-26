using Core;
using Data;
using Data.ScriptableObjects;

namespace GameLogic.Battle.Entity
{
    public class EntitySpawner
    {
        private readonly BattleEntityFactory _entityFactory;

        public EntitySpawner(BattleEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public BattleEntity SpawnHero(HeroData heroData, int boardIndex)
        {
            return _entityFactory.CreateHero(heroData, boardIndex);
        }

        public BattleEntity SpawnEnemy(EnemySO enemySo)
        {
            return _entityFactory.CreateEnemy(enemySo, Constants.EnemyBoardIndex); // Equal to hero number.
        }
    }
}