using Data.ScriptableObjects;
using UnityEngine;

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
            var position = GetHeroSpawnPosition(boardIndex);
            return _entityFactory.CreateHero(heroData, position);
        }

        public BattleEntity SpawnEnemy(EnemyData enemyData)
        {
            var position = GetEnemySpawnPosition();
            return _entityFactory.CreateEnemy(enemyData, position);
        }

        private Vector3 GetHeroSpawnPosition(int index)
        {
            // index based spawn position
            return new Vector3(-2 + index * 2, 0, 0);
        }

        private Vector3 GetEnemySpawnPosition()
        {
            return Vector3.zero;
        }
    }
}