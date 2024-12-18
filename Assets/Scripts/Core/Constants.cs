namespace Core
{
    public static class Constants
    {
        public const int MaxSelectedHeroes = 3;
        public const int TotalHeroes = 10;
        public const int TotalEnemies = 15;
        public const int TotalLevels = 15;

        #region Entity
        
        public const int EntityDefaultHealth = 100;
        public const int EntityDefaultAttackPower = 10;

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
    }
}
