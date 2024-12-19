﻿using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameLogicEventManager")]
[assembly: InternalsVisibleTo("UIEventManager")]
namespace Core.EventManager.Internal
{
    internal static class GameLogicEventsInternal
    {
        internal static event Action OnBattleSceneLoaded;
        internal static event Action OnHeroSelectionSceneLoaded;

        internal static void BroadcastBattleSceneLoaded()
        {
            OnBattleSceneLoaded?.Invoke();
        }
        
        internal static void BroadcastHeroSelectionSceneLoaded()
        {
            OnHeroSelectionSceneLoaded?.Invoke();
        }
    }
}