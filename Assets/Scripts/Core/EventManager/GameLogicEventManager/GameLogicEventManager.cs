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
        public static Action BroadcastBattleComplete(bool victory, List<int> aliveHeroes = null) => () => GameLogicInvokedEvents.BroadcastBattleComplete(victory, aliveHeroes);
        public static Action BroadcastPlayerTurnEnded => GameLogicInvokedEvents.BroadcastPlayerTurnEnded;
        public static Action BroadcastOpponentTurnEnded => GameLogicInvokedEvents.BroadcastOpponentTurnEnded;
        public static Action BroadcastEntityAttacked(int boardIndex) => () => GameLogicInvokedEvents.BroadcastEntityAttacked(boardIndex);
        public static Action BroadcastEntityDamaged(int boardIndex, float damage, float targetHealth) => () => GameLogicInvokedEvents.BroadcastEntityDamaged(boardIndex, damage, targetHealth);
        public static Action BroadcastEntityDied(int boardIndex) => () => GameLogicInvokedEvents.BroadcastEntityDied(boardIndex);
        public static Action BroadcastBattleSceneLoaded => GameLogicInvokedEvents.BroadcastBattleSceneLoaded;
        public static Action BroadcastHeroSelectionSceneLoaded => GameLogicInvokedEvents.BroadcastHeroSelectionSceneLoaded;

        #endregion

        #region Listeners
        
        public static event Action<bool, List<int>> OnBattleComplete
        {
            add => GameLogicInvokedEvents.OnBattleComplete += value;
            remove => GameLogicInvokedEvents.OnBattleComplete -= value;
        }
        
        public static event Action OnBattleSceneLoaded
        {
            add => GameLogicInvokedEvents.OnBattleSceneLoaded += value;
            remove => GameLogicInvokedEvents.OnBattleSceneLoaded -= value;
        }
        
        #region Invoked by UI Assembly
        
        public static event Action OnBattleStartRequested
        {
            add => UIInvokedEvents.OnBattleStartRequested += value;
            remove => UIInvokedEvents.OnBattleStartRequested -= value;
        }
        
        public static event Action OnReturnToHeroSelectionRequested
        {
            add => UIInvokedEvents.OnReturnToHeroSelectionRequested += value;
            remove => UIInvokedEvents.OnReturnToHeroSelectionRequested -= value;
        }
        
        public static event Action<List<int>> OnHeroesUpdateRequested
        {
            add => UIInvokedEvents.OnHeroesUpdateRequested += value;
            remove => UIInvokedEvents.OnHeroesUpdateRequested -= value;
        }
        
        public static event Action<int> OnPlayerAttackRequested
        {
            add => UIInvokedEvents.OnPlayerAttackRequested += value;
            remove => UIInvokedEvents.OnPlayerAttackRequested -= value;
        }

        #endregion

        #endregion
    }
}