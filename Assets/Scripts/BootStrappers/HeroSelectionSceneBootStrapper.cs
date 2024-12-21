using Core.BootStrapper;
using UI.Controllers;
using UnityEngine;

namespace BootStrappers
{
    public class HeroSelectionSceneBootStrapper: SceneBootStrapper
    {
        [SerializeField] private GameObject heroSelectionCanvasPrefab;

        public override void Initialize()
        {
            if (heroSelectionCanvasPrefab == null) return;
            
            var mainBootStrapper = GameBootStrapper as MainBootStrapper;
            if (mainBootStrapper == null) return;
            
            var gameState = mainBootStrapper.GetGameState();
            var entityService = mainBootStrapper.GetEntityService();
            
            var heroSelectionSceneController = Instantiate(heroSelectionCanvasPrefab, transform).GetComponent<HeroSelectionSceneController>();
            heroSelectionSceneController.Initialize(gameState, entityService);
        }

        protected override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}