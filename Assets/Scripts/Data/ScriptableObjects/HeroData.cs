using Core;
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
    }
}