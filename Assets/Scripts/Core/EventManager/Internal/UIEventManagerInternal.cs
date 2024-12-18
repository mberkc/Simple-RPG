using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UIEventManager")]
[assembly: InternalsVisibleTo("GameLogicEventManager")]
namespace EventManager.Internal
{
    internal static class UIEventManagerInternal
    {
        internal static event Action OnBattleStartRequested;

        internal static void RaiseBattleStartRequested()
        {
            OnBattleStartRequested?.Invoke();
        }
    }
}