using System;

namespace Core.BootStrapper
{
    /// <summary>
    /// Scene Bootstrapper exists until scene changes.
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
        
        protected override void Start()
        {
            // Define Start Behavior on subclass
        }
        
        public override void Initialize()
        {
            throw new NotImplementedException();
        }
        
        public virtual void OnDestroy()
        {
            throw new NotImplementedException();
        }
    }
}