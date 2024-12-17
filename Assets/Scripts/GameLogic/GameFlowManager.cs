using Data.ScriptableObjects;

namespace GameLogic
{
    public static class GameFlowManager
    {
        public static void StartBattle(HeroData[] selectedHeroes, EnemyData enemyToFace)
        {
            // Update GameState with selected heroes & enemy to face
            GameState.SelectedHeroes = selectedHeroes;
            GameState.EnemyToFace = enemyToFace;

            // Load the Battle Scene
            SceneLoader.LoadBattleScene();
        }
    }
}