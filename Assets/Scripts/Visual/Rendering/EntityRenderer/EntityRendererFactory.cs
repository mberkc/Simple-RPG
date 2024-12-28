using System;
using Core;
using Data;
using Data.ScriptableObjects;
using UnityEngine;
using Visual.Rendering.DamageValue;
using Object = UnityEngine.Object;

namespace Visual.Rendering.EntityRenderer
{
    /// <summary>
    /// Creates and Initializes Entity Renderer with Entity Data
    /// </summary>
    public class EntityRendererFactory
    {
        private readonly GameObject _heroPrefab;
        private readonly GameObject _enemyPrefab;
        private readonly DamageValueSpawner _damageValueSpawner;

        public EntityRendererFactory(GameObject heroPrefab, GameObject enemyPrefab, DamageValueSpawner damageValueSpawner)
        {
            _heroPrefab = heroPrefab;
            _enemyPrefab = enemyPrefab;
            _damageValueSpawner = damageValueSpawner;
        }
        
        public BattleHeroRenderer CreateHero(HeroData heroData, Transform parent, int boardIndex, Action<int> onHeroSelected, Action<BattleHeroRenderer> onHeroHold)
        {
            var rendererObject = Object.Instantiate(_heroPrefab, parent);
            var renderer = rendererObject.GetComponent<BattleHeroRenderer>();
            renderer.Initialize(heroData, boardIndex, _damageValueSpawner, onHeroSelected, onHeroHold);
            return renderer;
        }

        public BattleEnemyRenderer CreateEnemy(EnemySO enemySo, Transform parent)
        {
            var rendererObject = Object.Instantiate(_enemyPrefab, parent);
            var renderer = rendererObject.GetComponent<BattleEnemyRenderer>();
            renderer.Initialize(enemySo, Constants.EnemyBoardIndex, _damageValueSpawner);
            return renderer;
        }
    }
}