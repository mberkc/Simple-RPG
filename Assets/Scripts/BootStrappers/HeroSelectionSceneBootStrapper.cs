using Core.BootStrapper;
using UnityEngine;

namespace BootStrappers
{
    public class HeroSelectionSceneBootStrapper: SceneBootStrapper
    {
        [SerializeField] private GameObject heroSelectionCanvasPrefab;

        protected override void InitializeScene()
        {
            if (heroSelectionCanvasPrefab != null)
            {
                Instantiate(heroSelectionCanvasPrefab, transform);
            }
        }

        public override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}