using Data.ScriptableObjects;
using UnityEngine;

namespace GameLogic.Battle.Entity
{
    public class BattleEntityFactory
    {
        private readonly GameObject _heroPrefab;
        private readonly GameObject _enemyPrefab;

        public BattleEntityFactory(GameObject heroPrefab, GameObject enemyPrefab)
        {
            _heroPrefab = heroPrefab;
            _enemyPrefab = enemyPrefab;
        }

        public BattleEntity CreateHero(HeroData heroData, Vector3 position)
        {
            //var heroInstance = Object.Instantiate(_heroPrefab, position, Quaternion.identity);
            //var heroComponent = heroInstance.GetComponent<HeroManager>();
            //heroComponent.Initialize(heroData);
            return new BattleEntity(
                heroData.EntityName, 
                heroData.Index,
                heroData.Health,
                heroData.AttackPower);
        }

        public BattleEntity CreateEnemy(EnemyData enemyData, Vector3 position)
        {
            //var enemyInstance = Object.Instantiate(_enemyPrefab, position, Quaternion.identity);
            //var enemyComponent = enemyInstance.GetComponent<EnemyController>();
            //enemyComponent.Initialize(enemyData);
            return new BattleEntity(
                enemyData.EntityName, 
                enemyData.Index,
                enemyData.Health,
                enemyData.AttackPower);
        }
    }
}