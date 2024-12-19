using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.BootStrapper
{
    /// <summary>
    /// Initializes & Manages the scene. Scene Bootstrapper exists until scene changes.
    /// </summary>
    public abstract class SceneBootStrapper : BootStrapper
    {
        /// <summary>
        /// Don't override if it's not required!
        /// </summary>
        protected override void Awake()
        {
            Initialize();
        }
        
        /// <summary>
        /// Don't override if it's not required!
        /// </summary>
        public override async void Initialize()
        {
            try
            {
                await WaitGameBootstrapperInitialization();
                InitializeScene();
                Debug.Log($"{GetType()} initialized!");
            }
            catch (Exception e)
            {
                Debug.LogError($"{GetType()} initialization failed! Exception: {e.Message}");
            }
        }
        
        private async Task WaitGameBootstrapperInitialization()
        {
            var gameBootStrapper = GameBootStrapper.Instance;
            if(gameBootStrapper == null)
            {
                Debug.LogError("GameBootStrapper doesn't exist!");
                return;
            }

            if (!gameBootStrapper.IsInitialized)
            {
                Debug.Log("Waiting for GameBootStrapper initialization...");
                await gameBootStrapper.InitializationTask;
            }
        }
        
        protected abstract void InitializeScene();
        
        public virtual void OnDestroy()
        {
            throw new NotImplementedException();
        }
    }
}