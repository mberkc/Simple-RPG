using UnityEngine;

namespace Core.BootStrapper
{
    /// <summary>
    /// Don't extend this on different assembly.
    /// </summary>
    public abstract class BootStrapper : MonoBehaviour, IBootStrapper
    {
        public bool IsEnabled => enabled;

        /// <summary>
        /// Override for Initialize Behavior
        /// </summary>
        public abstract void Initialize();

        protected abstract void Awake();
        
        /// <summary>
        /// Override for Start Behavior
        /// </summary>
        protected abstract void Start();
    }
}