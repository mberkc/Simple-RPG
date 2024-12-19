using Core;
using UnityEditor;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewHero", menuName = "Game/Hero")]
    public class HeroData : EntityData
    {
        [SerializeField] private bool unlocked;
        [SerializeField] private int experience = 0;
        [SerializeField] private float level = 1;

        public bool Unlocked => unlocked;
        
        public int Experience => experience;
        
        public float Level => level;
        
        /// <summary>
        /// Returns false if unlocked already
        /// </summary>
        /// <returns></returns>
        public bool Unlock()
        {
            if(!unlocked) return false;
            
            unlocked = true;
            return true;
        }

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