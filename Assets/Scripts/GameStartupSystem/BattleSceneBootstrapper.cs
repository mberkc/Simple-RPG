using Data;
using GameLogic;
using GameLogic.Battle;
using GameStartupSystem.Bootstrapper;
using GameStartupSystem.Bootstrapper.Utility;
using UI.Controllers;
using UnityEngine;

namespace GameStartupSystem
{
    public class BattleSceneBootstrapper: SceneBootstrapper
    {
        [SerializeField] private GameObject battleCanvasPrefab;
        
        public override void Initialize()
        {
            if (battleCanvasPrefab == null) return;
            
            // Dependencies
            var gameStateManager = ServiceLocator.Resolve<GameStateManager>();
            var entityService = ServiceLocator.Resolve<EntityService>();
            
            var gameState = gameStateManager.GetGameStateUI();
            new BattleManager(gameState, entityService);

            var battleSceneController = Instantiate(battleCanvasPrefab, transform).GetComponent<BattleSceneController>();
            battleSceneController.Initialize(gameState, entityService);
        }

        protected override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}