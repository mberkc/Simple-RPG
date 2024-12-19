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
        
        public static Action RaiseBattleStartRequested => UIEventsInternal.RaiseBattleStartRequested;
        
        public static Action RaiseHeroesUpdateRequested(List<int> heroIndexes) => () => UIEventsInternal.RaiseHeroesUpdateRequested(heroIndexes);

        #endregion

        #region Listeners
        
        public static event Action OnBattleSceneLoaded
        {
            add => GameLogicEventsInternal.OnBattleSceneLoaded += value;
            remove => GameLogicEventsInternal.OnBattleSceneLoaded -= value;
        }
        
        public static event Action OnHeroSelectionSceneLoaded
        {
            add => GameLogicEventsInternal.OnHeroSelectionSceneLoaded += value;
            remove => GameLogicEventsInternal.OnHeroSelectionSceneLoaded -= value;
        }

        #endregion
    }
}