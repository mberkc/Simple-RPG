using Core;
using UnityEditor;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewHero", menuName = "Game/Hero")]
    public class HeroData : EntityData
    {
        [SerializeField] private int experience = 0;
        [SerializeField] private float level = 1;

        public int Experience => experience;
        
        public float Level => level;

        public void GainExperience(int amount)
        {
            experience += amount;
            if(experience < Constants.HeroLevelUpExperienceThreshold)
                return;
            
            LevelUp();
        }
        
        private void LevelUp()
        {
            level++;
            experience -= Constants.HeroLevelUpExperienceThreshold;
            Health *= Constants.HeroLevelUpHealthModifier;
            AttackPower *= Constants.HeroLevelUpAttackPowerModifier;
            
            // Notify
        }

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
            if (this.index != 0 || Application.isPlaying) return;
            
            var path = AssetDatabase.GetAssetPath(this);
            var folder = System.IO.Path.GetDirectoryName(path);
            var assets = AssetDatabase.FindAssets("t:HeroData", new[] { folder });

            var index = assets.Length-1;
            SetIndexAndName(index, baseName);
        }
        
#endif
    }
}