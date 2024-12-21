using Data;
using GameLogic;
using GameStartupSystem.Bootstrapper;
using GameStartupSystem.Bootstrapper.Utility;
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

            // Dependencies
            var gameStateManager = ServiceLocator.Resolve<GameStateManager>();
            var entityService = ServiceLocator.Resolve<EntityService>();
            
            var gameState = gameStateManager.GetGameStateUI();
            
            var heroSelectionSceneController = Instantiate(heroSelectionCanvasPrefab, transform).GetComponent<HeroSelectionSceneController>();
            heroSelectionSceneController.Initialize(gameState, entityService);
        }

        protected override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}