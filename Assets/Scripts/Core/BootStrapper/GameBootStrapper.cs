using System;
using System.Threading.Tasks;

namespace Core.BootStrapper
{
    /// <summary>
    /// Initializes & manages the game. Game Bootstrapper exists forever.
    /// </summary>
    public abstract class GameBootStrapper : BootStrapper
    {
        public static GameBootStrapper Instance;
        
        public bool IsInitialized => InitializationCompletionSource.Task.IsCompletedSuccessfully;
        public Task InitializationTask => InitializationCompletionSource.Task;
        protected readonly TaskCompletionSource<bool> InitializationCompletionSource = new();


        /// <summary>
        /// Don't override if it's not required!
        /// </summary>
        protected override void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            Initialize();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
