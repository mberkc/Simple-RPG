using UnityEngine;

namespace Core.Initializable
{
    public abstract class Initializable : IInitializable
    {
        private bool isInitialized = false;
        
        public void Initialize()
        {
            if (isInitialized) return;
            
            isInitialized = true;
            SubscribeEvents();
            Debug.Log("Initializable initialized.");
        }

        public void Cleanup()
        {
            if (!isInitialized) return;
            
            isInitialized = false;
            UnSubscribeEvents();
            Debug.Log("Initializable cleaned up.");
        }

        protected abstract void SubscribeEvents();
        
        protected abstract void UnSubscribeEvents();
    }
}