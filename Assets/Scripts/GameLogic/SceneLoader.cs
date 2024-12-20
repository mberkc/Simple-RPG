using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace GameLogic
{
    
    /// <summary>
    /// Obsolete Class
    /// </summary>
    public static class SceneLoader
    {
        public static async Task LoadBattleSceneAsync()
        {
            // Additional game logic before scene transition (if needed)
            Debug.Log("Loading Battle Scene...");
            var task = SceneManager.LoadSceneAsync(Constants.BattleSceneIndex);
            while (task is { isDone: false })
                await Task.Yield();
            Debug.Log("Battle Scene Loaded!");
        }

        public static async Task LoadHeroSelectionSceneAsync()
        {
            // Additional game logic before scene transition (if needed)
            Debug.Log("Loading Hero Selection Scene...");
            var task = SceneManager.LoadSceneAsync(Constants.HeroSelectionSceneIndex);
            while (task is { isDone: false })
                await Task.Yield();
            Debug.Log("Hero Selection Scene Loaded!");
        }
    }
}