using UnityEngine;

namespace Core
{
    public static class Constants
    {
        public const int MaxSelectedHeroes = 3;
        public const int TotalHeroes = 10;
        public const int TotalEnemies = 15;
        public const int TotalLevels = 15;
        public const int UnlockThreshold = 5;

        #region Entity
        
        public const int EntityDefaultHealth = 100;
        public const int EntityDefaultAttackPower = 10;

        public const int EnemyBoardIndex = 3;

        #region Hero

        public const int HeroLevelUpExperienceThreshold = 5;
        public const float HeroLevelUpHealthModifier = 1.1f;
        public const float HeroLevelUpAttackPowerModifier = 1.1f;

        #endregion

        #endregion

        #region Animation
        
        public const float SlowAnimationSpeed = 0.5f;
        public const float NormalAnimationSpeed = 0.3f;
        public const float FastAnimationSpeed = 0.1f;

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
