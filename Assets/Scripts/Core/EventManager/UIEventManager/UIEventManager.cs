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
        public static Action RaiseReturnToHeroSelectionRequested => UIInvokedEvents.RaiseReturnToHeroSelectionRequested;
        public static Action RaiseHeroesUpdateRequested(List<int> heroIndexes) => () => UIInvokedEvents.RaiseHeroesUpdateRequested(heroIndexes);
        public static Action RaisePlayerAttackRequested(int attackerBoardIndex) => () => UIInvokedEvents.RaisePlayerAttackRequested(attackerBoardIndex);
        
        #endregion

        #region Listeners

        #region Invoked by GameLogic Assembly
        
        public static event Action<bool, List<int>> OnBattleComplete
        {
            add => GameLogicInvokedEvents.OnBattleComplete += value;
            remove => GameLogicInvokedEvents.OnBattleComplete -= value;
        }
        
        public static event Action OnPlayerTurnEnded
        {
            add => GameLogicInvokedEvents.OnPlayerTurnEnded += value;
            remove => GameLogicInvokedEvents.OnPlayerTurnEnded -= value;
        }
        
        public static event Action OnOpponentTurnEnded
        {
            add => GameLogicInvokedEvents.OnOpponentTurnEnded += value;
            remove => GameLogicInvokedEvents.OnOpponentTurnEnded -= value;
        }
        
        public static event Action<int> OnEntityAttacked
        {
            add => GameLogicInvokedEvents.OnEntityAttacked += value;
            remove => GameLogicInvokedEvents.OnEntityAttacked -= value;
        }
        
        public static event Action<int, float, float>  OnEntityDamaged
        {
            add => GameLogicInvokedEvents.OnEntityDamaged += value;
            remove => GameLogicInvokedEvents.OnEntityDamaged -= value;
        }
        
        public static event Action<int> OnEntityDied
        {
            add => GameLogicInvokedEvents.OnEntityDied += value;
            remove => GameLogicInvokedEvents.OnEntityDied -= value;
        }

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