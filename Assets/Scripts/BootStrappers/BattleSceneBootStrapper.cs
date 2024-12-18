using Core.BootStrapper;
using UnityEngine;

namespace BootStrappers
{
    public class BattleSceneBootStrapper: SceneBootStrapper
    {
        [SerializeField] private GameObject battleCanvasPrefab;

        public override void Initialize()
        {
            if (battleCanvasPrefab != null)
            {
                Instantiate(battleCanvasPrefab, transform);
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