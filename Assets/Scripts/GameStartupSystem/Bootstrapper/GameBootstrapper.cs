using System.Threading.Tasks;
using UnityEngine;

namespace GameStartupSystem.Bootstrapper
{
    /// <summary>
    /// Initializes & manages the game. Game Bootstrapper exists forever. Only one Game Bootstrapper can exist!
    /// </summary>
    public abstract class GameBootstrapper : Bootstrapper
    {
        public static GameBootstrapper Instance;
        
        public bool IsInitialized => InitializationCompletionSource.Task.IsCompletedSuccessfully;
        public Task InitializationTask => InitializationCompletionSource.Task;
        protected readonly TaskCompletionSource<bool> InitializationCompletionSource = new();
        
        /// <summary>
        /// Don't override if it's not required!
        /// </summary>
        public override void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.Log($"Duplicate GameBootstrapper is found, deleting... Name: {name}. Please check if's the original one. If the scene is reloaded it might try to create a new one, if that's the case just ignore this.");
                Destroy(gameObject);
                return;
            }

            Initialize();
        }
    }
}
