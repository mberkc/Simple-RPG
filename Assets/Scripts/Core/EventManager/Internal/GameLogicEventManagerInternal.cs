using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameLogicEventManager")]
[assembly: InternalsVisibleTo("UIEventManager")]
namespace EventManager.Internal
{
    internal static class GameLogicEventManagerInternal
    {
        internal static event Action OnBattleSceneLoaded;

        internal static void BroadcastBattleSceneLoaded()
        {
            OnBattleSceneLoaded?.Invoke();
        }
    }
}