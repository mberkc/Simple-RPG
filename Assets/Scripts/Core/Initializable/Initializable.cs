using Core.Progression;
using UnityEngine;

namespace Core.Initializable
{
    public abstract class Initializable : IInitializable
    {
        protected ProgressionService progressionService;
        private bool isInitialized = false;
        
        public void Initialize(ProgressionService progressionService)
        {
            if (isInitialized) return;
            
            isInitialized = true;
            this.progressionService = progressionService;
            SubscribeEvents();
            Debug.Log($"Initializable: {GetType()}  initialized.");
        }

        public void Cleanup()
        {
            if (!isInitialized) return;
            
            isInitialized = false;
            progressionService = null;
            UnSubscribeEvents();
            Debug.Log($"Initializable: {GetType()} cleaned up.");
        }

        protected abstract void SubscribeEvents();
        
        protected abstract void UnSubscribeEvents();
    }
}