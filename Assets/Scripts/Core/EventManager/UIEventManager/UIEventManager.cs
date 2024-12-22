using System;
using System.Collections.Generic;
using Core.EventManager.Internal;

namespace Core.EventManager.UIEventManager
{
    /// <summary>
    /// Event Manager for UI Assembly
    /// </summary>
    public static class UIEventManager
    {
        #region Invokers
        
        public static Action RaiseBattleStartRequested => UIInvokedEvents.RaiseBattleStartRequested;
        public static Action RaiseHeroesUpdateRequested(List<int> heroIndexes) => () => UIInvokedEvents.RaiseHeroesUpdateRequested(heroIndexes);

        #endregion

        #region Listeners

        #region Invoked by GameLogic Assembly

        public static event Action OnBattleSceneLoaded
        {
            add => GameLogicInvokedEvents.OnBattleSceneLoaded += value;
            remove => GameLogicInvokedEvents.OnBattleSceneLoaded -= value;
        }
        
        public static event Action OnHeroSelectionSceneLoaded
        {
            add => GameLogicInvokedEvents.OnHeroSelectionSceneLoaded += value;
            remove => GameLogicInvokedEvents.OnHeroSelectionSceneLoaded -= value;
        }

        #endregion

        #endregion
    }
}