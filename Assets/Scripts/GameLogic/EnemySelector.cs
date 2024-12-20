using Data;
using Data.ScriptableObjects;

namespace GameLogic
{
    /// <summary>
    /// Obsolete Class
    /// </summary>
    public static class EnemySelector
    {
        public static EnemyData GetEnemyForLevel(int level)
        {
            // related to index => level-1 because level starts from 1
            //return EntityDatabase.GetEnemyByIndex(level-1);
            // TODO
            return null;
        }
    }
}