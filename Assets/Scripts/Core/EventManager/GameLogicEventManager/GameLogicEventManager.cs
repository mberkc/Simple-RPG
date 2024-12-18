using System;
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

        #endregion

        #region Listeners
        
        public static event Action OnBattleStartRequested
        {
            add => UIEventsInternal.OnBattleStartRequested += value;
            remove => UIEventsInternal.OnBattleStartRequested -= value;
        }

        #endregion
    }
}