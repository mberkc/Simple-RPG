using Data.ScriptableObjects;
using UnityEngine;

namespace Visual.Rendering
{
    /// <summary>
    /// Creates and Initializes Entity Renderer with Entity Data
    /// </summary>
    public class EntityRendererFactory
    {
        private readonly GameObject _heroPrefab;
        private readonly GameObject _enemyPrefab;

        public EntityRendererFactory(GameObject heroPrefab, GameObject enemyPrefab)
        {
            _heroPrefab = heroPrefab;
            _enemyPrefab = enemyPrefab;
        }
        
        public BattleHeroRenderer CreateHero(HeroData heroData, Transform parent, int boardIndex)
        {
            var rendererObject = Object.Instantiate(_heroPrefab, parent);
            var renderer = rendererObject.GetComponent<BattleHeroRenderer>();
            renderer.Initialize(heroData, boardIndex);
            return renderer;
        }

        public BattleEntityRenderer CreateEnemy(EnemyData enemyData, Transform parent)
        {
            var rendererObject = Object.Instantiate(_enemyPrefab, parent);
            var renderer = rendererObject.GetComponent<BattleEntityRenderer>();
            renderer.Initialize(enemyData);
            return renderer;
        }
    }
}