using GameLogic;
using GameStartupSystem.Bootstrapper;
using GameStartupSystem.Bootstrapper.Utility;
using UnityEngine;
using Visual.Controllers;

namespace GameStartupSystem
{
    public class HeroSelectionSceneBootstrapper: SceneBootstrapper
    {
        [SerializeField] private GameObject heroSelectionCanvasPrefab;

        public override void Initialize()
        {
            if (heroSelectionCanvasPrefab == null) return;

            // Dependencies
            var userDataManager = ServiceLocator.Resolve<UserDataManager>();
            
            var heroSelectionSceneController = Instantiate(heroSelectionCanvasPrefab, transform).GetComponent<HeroSelectionSceneController>();
            heroSelectionSceneController.Initialize(userDataManager.GetUserDataVisual);
        }

        protected override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}