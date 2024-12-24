using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core
{
    /// <summary>
    /// Manages scene transitions.
    /// </summary>
    public class SceneTransitionService
    {
        public async Task LoadSceneAsync(int sceneIndex, Action preLoad = null, Action postLoad = null)
        {
            preLoad?.Invoke();
            var task = SceneManager.LoadSceneAsync(sceneIndex);
            while (task is { isDone: false }) await Task.Yield();
            
            postLoad?.Invoke();
        }
    }
}