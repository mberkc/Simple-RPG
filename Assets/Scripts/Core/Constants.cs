using UnityEngine;

namespace Core
{
    public static class Constants
    {
        public const string SaveFileName = "progression.txt";
        
        public const int MaxSelectedHeroes = 3;
        public const int TotalHeroes = 10;
        public const int UnlockThreshold = 5;
        
        /// <summary>
        /// Equal to Hero number on battle
        /// </summary>
        public const int EnemyBoardIndex = 3;

        #region Entity
        
        public const int EntityDefaultHealth = 100;
        public const int EntityDefaultAttackPower = 10;

        #region Hero

        public const int ExperiencePerBattle = 1;
        public const int HeroLevelUpExperienceThreshold = 5;
        public const float HeroLevelUpHealthModifier = 1.1f;
        public const float HeroLevelUpAttackPowerModifier = 1.1f;

        #endregion

        #endregion

        #region Animation
        
        public const float NormalAnimationDuration = 0.3f;
        public const float FastAnimationDuration = 0.1f;

        #endregion

        #region SceneManagement

        public const int HeroSelectionSceneIndex = 0;
        public const int BattleSceneIndex = 1;

        #endregion

        #region Visual

        public static readonly int ShaderColorPropertyId = Shader.PropertyToID("_Color"); // BaseColor?

        #endregion
    }
}
