using Core;
using Data.ScriptableObjects;

namespace GameLogic.State
{
    public static class GameState
    {
        public static HeroData[] SelectedHeroes = new HeroData[Constants.MaxSelectedHeroes];
        public static EnemyData EnemyToFace;
    }
}