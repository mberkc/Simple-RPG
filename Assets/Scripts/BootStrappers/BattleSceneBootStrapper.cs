using Core.BootStrapper;
using UnityEngine;

namespace BootStrappers
{
    public class BattleSceneBootStrapper: SceneBootStrapper
    {
        [SerializeField] private GameObject battleCanvasPrefab;
        
        protected override void InitializeScene()
        {
            if (battleCanvasPrefab != null)
            {
                Instantiate(battleCanvasPrefab, transform);
            }
        }

        public override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}