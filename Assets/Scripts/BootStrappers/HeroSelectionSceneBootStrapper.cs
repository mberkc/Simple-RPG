using Core.BootStrapper;
using UnityEngine;

namespace BootStrappers
{
    public class HeroSelectionSceneBootStrapper: SceneBootStrapper
    {
        [SerializeField] private GameObject heroSelectionCanvasPrefab;

        public override void Initialize()
        {
            if (heroSelectionCanvasPrefab != null)
            {
                Instantiate(heroSelectionCanvasPrefab, transform);
            }
        }
        
        protected override void Start()
        {
            // Start Behavior
        }

        public override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}