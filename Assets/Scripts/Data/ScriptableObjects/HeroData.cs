using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewHero", menuName = "Game/Hero")]
    public class HeroData : EntityData
    {
        [SerializeField] private float experience = 0;
        [SerializeField] private float level = 1;

        public float Experience => experience;
        
        public float Level => level;

        public void GainExperience()
        {
            if(++experience < 5)
                return;
            
            LevelUp();
        }
        
        private void LevelUp()
        {
            level++;
            experience = 0;
            Health *= 1.1f;
            AttackPower *= 1.1f;
        }
    }
}