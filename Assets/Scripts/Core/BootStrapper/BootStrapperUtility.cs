using System.Threading.Tasks;
using UnityEngine;

namespace Core.BootStrapper
{
    public static class BootStrapperUtility
    {
        private static GameBootStrapper _gameBootStrapper;

        internal static GameBootStrapper GameBootStrapper => _gameBootStrapper;

        public static async Task<bool> WaitForGameBootStrapperInitialization()
        {
            // Check if GameBootStrapper exists in the scene
            if (!TryGetGameBootStrapper())
            {
                Debug.LogError("GameBootStrapper doesn't exist in the scene! Add it to the scene!");
                return false;
            }

            // Wait for initialization if not already completed
            if (!_gameBootStrapper.IsInitialized)
            {
                Debug.Log("Waiting for GameBootStrapper initialization...");
                await _gameBootStrapper.InitializationTask;
            }

            return true;
        }

        private static bool TryGetGameBootStrapper()
        {
            // Tries setting on each call, because it can be destroyed.
            _gameBootStrapper = GameBootStrapper.Instance;
            if (_gameBootStrapper == null)
            {
                // instance can be checked asynchronously with delaying getting instance in a loop.
                // Cost of FindObjectOfType will be negligible, if we're calling it once.
                _gameBootStrapper = Object.FindObjectOfType<GameBootStrapper>();
                if (_gameBootStrapper == null)
                {
                    Debug.LogWarning("GameBootStrapper not found in the scene.");
                    return false;
                }
            }

            return true;
        }
    }
}