using System;

namespace Core.BootStrapper
{
    /// <summary>
    /// Game Bootstrapper exists forever.
    /// </summary>
    public abstract class GameBootStrapper : BootStrapper
    {
        private static GameBootStrapper _instance;

        /// <summary>
        /// Don't override if it's not required!
        /// </summary>
        protected override void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

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
    }
}
