using UnityEngine;

namespace Core.BootStrapper
{
    /// <summary>
    /// Don't extend this on different assembly.
    /// </summary>
    public abstract class BootStrapper : MonoBehaviour, IBootStrapper
    {
        public bool IsEnabled => enabled;
        
        public abstract void Initialize();

        public abstract void Awake();
    }
}