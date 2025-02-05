﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameLogicEventManager")]
[assembly: InternalsVisibleTo("UIEventManager")]
namespace Core.EventManager.Internal
{
    internal static class GameLogicInvokedEvents
    {
        internal static event Action<bool, List<int>> OnBattleComplete; // For Both Assemblies
        internal static event Action OnPlayerTurnStarted;
        internal static event Action OnPlayerTurnEnded;
        internal static event Action<int> OnEntityAttacked;
        internal static event Action<int, float, float> OnEntityDamaged;
        internal static event Action<int> OnEntityDied;
        internal static event Action OnBattleSceneLoaded; // For GameLogic Assembly
        
        internal static void BroadcastBattleComplete(bool victory, List<int> aliveHeroIndexes)
        {
            OnBattleComplete?.Invoke(victory, aliveHeroIndexes);
        }
        
        internal static void BroadcastPlayerTurnStarted()
        {
            OnPlayerTurnStarted?.Invoke();
        }
        
        internal static void BroadcastPlayerTurnEnded()
        {
            OnPlayerTurnEnded?.Invoke();
        }
        
        internal static void BroadcastEntityAttacked(int boardIndex)
        {
            OnEntityAttacked?.Invoke(boardIndex);
        }
        
        internal static void BroadcastEntityDamaged(int boardIndex, float damage, float targetHealth)
        {
            OnEntityDamaged?.Invoke(boardIndex, damage, targetHealth);
        }
        
        internal static void BroadcastEntityDied(int boardIndex)
        {
            OnEntityDied?.Invoke(boardIndex);
        }

        internal static void BroadcastBattleSceneLoaded()
        {
            OnBattleSceneLoaded?.Invoke();
        }
    }
}