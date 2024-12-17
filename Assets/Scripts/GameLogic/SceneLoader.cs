using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public static class SceneLoader
    {
        public static void LoadBattleScene()
        {
            // Additional game logic before scene transition (if needed)
            Debug.Log("Loading Battle Scene...");
            SceneManager.LoadScene(Constants.BattleSceneIndex);
        }

        public static void LoadHeroSelectionScene()
        {
            Debug.Log("Loading Hero Selection Scene...");
            SceneManager.LoadScene(Constants.HeroSelectionSceneIndex);
        }
    }
}