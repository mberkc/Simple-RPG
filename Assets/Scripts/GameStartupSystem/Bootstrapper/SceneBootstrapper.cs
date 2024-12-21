using System;
using UnityEngine;

namespace GameStartupSystem.Bootstrapper
{
    /// <summary>
    /// Initializes & Manages the scene. Scene Bootstrapper exists until scene changes.
    /// </summary>
    public abstract class SceneBootstrapper : Bootstrapper
    {
        protected static GameBootstrapper GameBootstrapper => BootstrapperUtility.GameBootstrapper;
        
        /// <summary>
        /// Don't override if it's not required!
        /// </summary>
        public override async void Awake()
        {
            try
            {
                if(!await BootstrapperUtility.WaitForGameBootstrapperInitialization()) return;
                
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