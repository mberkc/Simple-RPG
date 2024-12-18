using System;
using EventManager.Internal;

namespace EventManager.UIEventManager
{
    /// <summary>
    /// Event Manager for UI Assembly
    /// </summary>
    public static class UIEventManager
    {
        public static Action RaiseBattleStartRequested => UIEventManagerInternal.RaiseBattleStartRequested;

        public static event Action OnBattleSceneLoaded
        {
            add => GameLogicEventManagerInternal.OnBattleSceneLoaded += value;
            remove => GameLogicEventManagerInternal.OnBattleSceneLoaded -= value;
        }
    }
}