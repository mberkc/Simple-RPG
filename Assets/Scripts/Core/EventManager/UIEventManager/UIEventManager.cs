using System;
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
        
        //public static Action RaiseHeroesUpdateRequested(List<HeroData> selectedHeroes) => UIEventsInternal.RaiseHeroesUpdateRequested;

        #endregion

        #region Listeners
        
        public static event Action OnBattleSceneLoaded
        {
            add => GameLogicEventsInternal.OnBattleSceneLoaded += value;
            remove => GameLogicEventsInternal.OnBattleSceneLoaded -= value;
        }

        #endregion
    }
}