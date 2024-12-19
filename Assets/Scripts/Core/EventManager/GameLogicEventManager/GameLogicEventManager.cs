using System;
using System.Collections.Generic;
using Core.EventManager.Internal;

namespace Core.EventManager.GameLogicEventManager
{
    /// <summary>
    /// Event Manager for GameLogic Assembly
    /// </summary>
    public static class GameLogicEventManager
    {
        #region Invokers

        public static Action BroadcastBattleSceneLoaded => GameLogicEventsInternal.BroadcastBattleSceneLoaded;
        public static Action BroadcastHeroSelectionSceneLoaded => GameLogicEventsInternal.BroadcastHeroSelectionSceneLoaded;

        #endregion

        #region Listeners
        
        public static event Action OnBattleStartRequested
        {
            add => UIEventsInternal.OnBattleStartRequested += value;
            remove => UIEventsInternal.OnBattleStartRequested -= value;
        }
        
        public static event Action<List<int>> OnHeroesUpdateRequested
        {
            add => UIEventsInternal.OnHeroesUpdateRequested += value;
            remove => UIEventsInternal.OnHeroesUpdateRequested -= value;
        }

        #endregion
    }
}