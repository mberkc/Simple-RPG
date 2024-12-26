using UnityEditor;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewHero", menuName = "Game/Hero")]
    public class HeroSO : EntitySO
    {
#if UNITY_EDITOR
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            AssignSequentialIndexAndName("Hero");
        }
        
        protected override void AssignDefaults()
        {
            base.AssignDefaults();

            AssignSequentialIndexAndName("Hero");
        }

        private void AssignSequentialIndexAndName(string baseName)
        {
            if (this.index != -1 || Application.isPlaying) return;
            
            var path = AssetDatabase.GetAssetPath(this);
            var folder = System.IO.Path.GetDirectoryName(path);
            var assets = AssetDatabase.FindAssets("t:HeroData", new[] { folder });

            var index = assets.Length-1;
            SetIndexAndName(index, baseName);
        }
        
#endif
    }
}