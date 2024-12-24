using UnityEngine;

namespace GameStartupSystem.Bootstrapper
{
    /// <summary>
    /// Don't extend this on different assembly.
    /// </summary>
    public abstract class Bootstrapper : MonoBehaviour, IBootstrapper
    {
        public bool IsEnabled => enabled;
        
        public abstract void Initialize();

        public abstract void Awake();
    }
}