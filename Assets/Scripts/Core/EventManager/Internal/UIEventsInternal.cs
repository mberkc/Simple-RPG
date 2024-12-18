using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UIEventManager")]
[assembly: InternalsVisibleTo("GameLogicEventManager")]
namespace Core.EventManager.Internal
{
    internal static class UIEventsInternal
    {
        internal static event Action OnBattleStartRequested;
        //internal static event Action<List<HeroData>> OnHeroesUpdateRequested;

        internal static void RaiseBattleStartRequested()
        {
            OnBattleStartRequested?.Invoke();
        }
        
        internal static void RaiseHeroesUpdateRequested()
        {
            OnBattleStartRequested?.Invoke();
        }
    }
}