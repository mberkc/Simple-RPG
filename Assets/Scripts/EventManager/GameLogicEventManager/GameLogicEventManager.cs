using System;
using EventManager.Internal;

namespace EventManager.GameLogicEventManager
{
    /// <summary>
    /// Event Manager for GameLogic Assembly
    /// </summary>
    public static class GameLogicEventManager
    {
        public static Action BroadcastBattleSceneLoaded => GameLogicEventManagerInternal.BroadcastBattleSceneLoaded;

        public static event Action OnBattleStartRequested
        {
            add => UIEventManagerInternal.OnBattleStartRequested += value;
            remove => UIEventManagerInternal.OnBattleStartRequested -= value;
        }
    }
}