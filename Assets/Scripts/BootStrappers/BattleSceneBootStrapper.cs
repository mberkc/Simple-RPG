using Core.BootStrapper;
using GameLogic;
using UI.Controllers;
using UnityEngine;

namespace BootStrappers
{
    public class BattleSceneBootStrapper: SceneBootStrapper
    {
        [SerializeField] private GameObject battleCanvasPrefab;
        
        public override void Initialize()
        {
            if (battleCanvasPrefab == null) return;
            
            var mainBootStrapper = GameBootStrapper as MainBootStrapper;
            if (mainBootStrapper == null) return;
            
            var gameState = mainBootStrapper.GetGameState();
            var entityService = mainBootStrapper.GetEntityService();
            
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