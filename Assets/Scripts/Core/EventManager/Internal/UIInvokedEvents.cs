using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UIEventManager")]
[assembly: InternalsVisibleTo("GameLogicEventManager")]
namespace Core.EventManager.Internal
{
    internal static class UIInvokedEvents
    {
        internal static event Action OnBattleStartRequested;
        internal static event Action<List<int>> OnHeroesUpdateRequested;

        internal static void RaiseBattleStartRequested()
        {
            OnBattleStartRequested?.Invoke();
        }
        
        internal static void RaiseHeroesUpdateRequested(List<int> heroIndexes)
        {
            OnHeroesUpdateRequested?.Invoke(heroIndexes);
        }
    }
}