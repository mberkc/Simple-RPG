namespace GameLogic
{
    /// <summary>
    /// Obsolete Class
    /// </summary>
    public static class LevelManager
    {
        public static int CurrentLevel { get; private set; } = 1;

        public static void AdvanceLevel()
        {
            CurrentLevel++;
        }

        public static void SetLevel(int level)
        {
            CurrentLevel = level;
        }
    }
}