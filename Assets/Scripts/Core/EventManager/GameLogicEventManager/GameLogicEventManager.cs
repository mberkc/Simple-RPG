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
        public static Action BroadcastBattleComplete(bool isWin, List<int> aliveHeroes = null) => () => GameLogicInvokedEvents.BroadcastBattleComplete(isWin, aliveHeroes);
        public static Action BroadcastBattleSceneLoaded => GameLogicInvokedEvents.BroadcastBattleSceneLoaded;
        public static Action BroadcastHeroSelectionSceneLoaded => GameLogicInvokedEvents.BroadcastHeroSelectionSceneLoaded;

        #endregion

        #region Listeners
        
        public static event Action<bool, List<int>> OnBattleComplete
        {
            add => GameLogicInvokedEvents.OnBattleComplete += value;
            remove => GameLogicInvokedEvents.OnBattleComplete -= value;
        }
        
        #region Invoked by UI Assembly
        
        public static event Action OnBattleStartRequested
        {
            add => UIInvokedEvents.OnBattleStartRequested += value;
            remove => UIInvokedEvents.OnBattleStartRequested -= value;
        }
        
        public static event Action<List<int>> OnHeroesUpdateRequested
        {
            add => UIInvokedEvents.OnHeroesUpdateRequested += value;
            remove => UIInvokedEvents.OnHeroesUpdateRequested -= value;
        }

        #endregion

        #endregion
    }
}