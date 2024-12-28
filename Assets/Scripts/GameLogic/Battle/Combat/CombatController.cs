using System;
using System.Threading.Tasks;
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

        /// <summary>
        /// Returns true if attack is successful
        /// </summary>
        /// <param name="attackerIndex"></param>
        public async Task<bool> HandlePlayerAttack(int attackerIndex)
        {
            try
            {
                var attacker = _playerManager.GetHeroEntities[attackerIndex];
                var target = _opponentManager.GetEnemyEntity;
                Debug.Log($"Player's {attacker.EntityName} is trying to attacking {target.EntityName}.");

                if (!await _attackHandler.ExecuteAttack(attacker, target)) return false;
                
                OnPlayerAttackFinished?.Invoke();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"PlayerAttack failed! Exception: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// Returns true if attack is successful
        /// </summary>
        /// <param name="attackerIndex"></param>
        public async Task<bool> HandleOpponentAttack()
        {
            try
            {
                // We can implement targeting handler if these grow.
                var attacker = _opponentManager.GetEnemyEntity;
                var availableTargets = _playerManager.GetHeroEntities;
                var target = await _opponentManager.GetTarget(availableTargets);
                Debug.Log($"Opponent's {attacker.EntityName} is trying to attacking {target.EntityName}.");
                
                if(!await _attackHandler.ExecuteAttack(attacker, target)) return false;
                
                OnOpponentAttackFinished?.Invoke();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"OpponentAttack failed! Exception: {e.Message}");
                return false;
            }
        }
    }
}