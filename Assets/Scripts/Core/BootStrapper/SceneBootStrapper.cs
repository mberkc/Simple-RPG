using System;
using UnityEngine;

namespace Core.BootStrapper
{
    /// <summary>
    /// Initializes & Manages the scene. Scene Bootstrapper exists until scene changes.
    /// </summary>
    public abstract class SceneBootStrapper : BootStrapper
    {
        protected static GameBootStrapper GameBootStrapper => BootStrapperUtility.GameBootStrapper;
        
        /// <summary>
        /// Don't override if it's not required!
        /// </summary>
        public override async void Awake()
        {
            try
            {
                if(!await BootStrapperUtility.WaitForGameBootStrapperInitialization()) return;
                
                Initialize();
                Debug.Log($"{GetType()} initialized!");
            }
            catch (Exception e)
            {
                Debug.LogError($"{GetType()} initialization failed! Exception: {e.Message}");
            }
        }
        
        protected abstract void OnDestroy();
    }
}