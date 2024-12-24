using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameLogicEventManager")]
[assembly: InternalsVisibleTo("UIEventManager")]
namespace Core.EventManager.Internal
{
    internal static class GameLogicInvokedEvents
    {
        internal static event Action<bool, List<int>> OnBattleComplete;
        internal static event Action OnPlayerTurnEnded;
        internal static event Action OnOpponentTurnEnded;
        internal static event Action OnBattleSceneLoaded;
        internal static event Action OnHeroSelectionSceneLoaded;
        
        internal static void BroadcastBattleComplete(bool victory, List<int> aliveHeroIndexes)
        {
            OnBattleComplete?.Invoke(victory, aliveHeroIndexes);
        }
        
        internal static void BroadcastPlayerTurnEnded()
        {
            OnPlayerTurnEnded?.Invoke();
        }
        
        internal static void BroadcastOpponentTurnEnded()
        {
            OnOpponentTurnEnded?.Invoke();
        }

        internal static void BroadcastBattleSceneLoaded()
        {
            OnBattleSceneLoaded?.Invoke();
        }
        
        internal static void BroadcastHeroSelectionSceneLoaded()
        {
            OnHeroSelectionSceneLoaded?.Invoke();
        }
    }
}