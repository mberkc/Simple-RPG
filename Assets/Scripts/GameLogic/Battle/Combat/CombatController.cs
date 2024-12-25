using System;
using UnityEngine;

namespace GameLogic.Battle.Combat
{
    /// <summary>
    /// Handles target selection and attack execution.
    /// </summary>
    public class CombatController
    {
        public event Action OnPlayerAttackFinished;
        public event Action OnOpponentAttackFinished;
        
        private readonly AttackHandler _attackHandler;
        private readonly PlayerManager _playerManager;
        private readonly OpponentManager _opponentManager;

        public CombatController(AttackHandler attackHandler, PlayerManager playerManager, OpponentManager opponentManager)
        {
            _attackHandler = attackHandler;
            _playerManager = playerManager;
            _opponentManager = opponentManager;
        }

        // We can implement targeting handler if these grow.
        public void HandlePlayerAttack(int attackerIndex)
        {
            var attacker = _playerManager.GetHeroEntities[attackerIndex];
            var target = _opponentManager.GetEnemyEntity;
            Debug.Log($"Player's {attacker.EntityName} is trying to attacking {target.EntityName}.");
            
            if(!_attackHandler.ExecuteAttack(attacker, target)) return;
            
            OnPlayerAttackFinished?.Invoke();
        }

        public async void HandleOpponentAttack()
        {
            try
            {
                var attacker = _opponentManager.GetEnemyEntity;
                var availableTargets = _playerManager.GetHeroEntities;
                var target = await _opponentManager.GetTarget(availableTargets);
                Debug.Log($"Opponent's {attacker.EntityName} is trying to attacking {target.EntityName}.");
                
                if(!_attackHandler.ExecuteAttack(attacker, target)) return;
                
                OnOpponentAttackFinished?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError($"OpponentAttack failed! Exception: {e.Message}");
            }
        }
    }
}