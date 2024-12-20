using Core.BootStrapper;
using GameLogic;
using UI.Controllers;
using UnityEngine;

namespace BootStrappers
{
    public class BattleSceneBootStrapper: SceneBootStrapper
    {
        [SerializeField] private GameObject battleCanvasPrefab;
        
        protected override void InitializeScene()
        {
            if (battleCanvasPrefab == null) return;
            
            var mainBootStrapper = GameBootStrapper as MainBootStrapper;
            var gameState = mainBootStrapper.GetGameState();
            var entityService = mainBootStrapper.GetEntityService();
            
            var battleManager = new BattleManager();
            battleManager.Initialize(gameState, entityService);

            var battleSceneController = Instantiate(battleCanvasPrefab, transform).GetComponent<BattleSceneController>();
            battleSceneController.Initialize(gameState, entityService);
        }

        public override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}