using GameStartupSystem.Bootstrapper;
using UI.Controllers;
using UnityEngine;

namespace GameStartupSystem
{
    public class HeroSelectionSceneBootstrapper: SceneBootstrapper
    {
        [SerializeField] private GameObject heroSelectionCanvasPrefab;

        public override void Initialize()
        {
            if (heroSelectionCanvasPrefab == null) return;
            
            var mainBootstrapper = GameBootstrapper as MainBootstrapper;
            if (mainBootstrapper == null) return;
            
            var gameState = mainBootstrapper.GetGameState();
            var entityService = mainBootstrapper.GetEntityService();
            
            var heroSelectionSceneController = Instantiate(heroSelectionCanvasPrefab, transform).GetComponent<HeroSelectionSceneController>();
            heroSelectionSceneController.Initialize(gameState, entityService);
        }

        protected override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}