using Core.BootStrapper;
using UI.Controllers;
using UnityEngine;

namespace BootStrappers
{
    public class HeroSelectionSceneBootStrapper: SceneBootStrapper
    {
        [SerializeField] private GameObject heroSelectionCanvasPrefab;

        protected override void InitializeScene()
        {
            if (heroSelectionCanvasPrefab == null) return;
            
            var mainBootStrapper = GameBootStrapper as MainBootStrapper;
            var gameState = mainBootStrapper.GetGameState();
            var entityService = mainBootStrapper.GetEntityService();
            var heroSelectionSceneController = Instantiate(heroSelectionCanvasPrefab, transform).GetComponent<HeroSelectionSceneController>();
            heroSelectionSceneController.Initialize(gameState, entityService);
        }

        public override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}