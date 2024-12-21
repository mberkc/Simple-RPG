using System.Threading.Tasks;
using UnityEngine;

namespace GameStartupSystem.Bootstrapper
{
    public static class BootstrapperUtility
    {
        private static GameBootstrapper _gameBootstrapper;

        internal static GameBootstrapper GameBootstrapper => _gameBootstrapper;

        public static async Task<bool> WaitForGameBootstrapperInitialization()
        {
            // Check if GameBootStrapper exists in the scene
            if (!TryGetGameBootstrapper())
            {
                Debug.LogError("Game Bootstrapper doesn't exist in the scene! Add it to the scene!");
                return false;
            }

            // Wait for initialization if not already completed
            if (!_gameBootstrapper.IsInitialized)
            {
                Debug.Log("Waiting for Game Bootstrapper initialization...");
                await _gameBootstrapper.InitializationTask;
            }

            return true;
        }

        private static bool TryGetGameBootstrapper()
        {
            // Tries setting on each call, because it can be destroyed.
            if (_gameBootstrapper == null)
            {
                // instance can be checked asynchronously with delaying getting instance in a loop.
                // Cost of FindObjectOfType will be negligible, if we're calling it once.
                _gameBootstrapper = Object.FindObjectOfType<GameBootstrapper>();
                if (_gameBootstrapper == null)
                {
                    Debug.LogWarning("Game Bootstrapper not found in the scene.");
                    return false;
                }
            }

            return true;
        }
    }
}