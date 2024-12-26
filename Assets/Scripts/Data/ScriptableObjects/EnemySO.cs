using UnityEditor;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy")]
    public class EnemySO : EntitySO
    {
#if UNITY_EDITOR
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            AssignSequentialIndexAndName("Enemy");
        }
        
        protected override void AssignDefaults()
        {
            base.AssignDefaults();

            AssignSequentialIndexAndName("Enemy");
        }

        private void AssignSequentialIndexAndName(string baseName)
        {
            if (this.index != -1 || Application.isPlaying) return;
            
            var path = AssetDatabase.GetAssetPath(this);
            var folder = System.IO.Path.GetDirectoryName(path);
            var assets = AssetDatabase.FindAssets("t:EnemyData", new[] { folder });

            var index = assets.Length-1;
            SetIndexAndName(index, baseName);
        }
        
#endif
    }
}