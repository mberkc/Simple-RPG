using GameLogic;
using GameStartupSystem.Bootstrapper;
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
            
            var mainBootstrapper = GameBootstrapper as MainBootstrapper;
            if (mainBootstrapper == null) return;
            
            var gameState = mainBootstrapper.GetGameState();
            var entityService = mainBootstrapper.GetEntityService();
            
            var battleManager = new BattleManager();
            battleManager.Initialize(gameState, entityService);

            var battleSceneController = Instantiate(battleCanvasPrefab, transform).GetComponent<BattleSceneController>();
            battleSceneController.Initialize(gameState, entityService);
        }

        protected override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}