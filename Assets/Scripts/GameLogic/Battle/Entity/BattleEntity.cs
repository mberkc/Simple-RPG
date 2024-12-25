using Core.EventManager.GameLogicEventManager;
using UnityEngine;

namespace GameLogic.Battle.Entity
{
    public class BattleEntity
    {
        public readonly int BoardIndex;
        public readonly int Index;
        public readonly string EntityName;
        public readonly float CurrentAttackPower; // can be changed later but currently AP is static during battle.
        public float CurrentHealth { get; private set; }
        
        public bool IsAlive => CurrentHealth > 0;

        public BattleEntity(string name, int index, float health, float attackPower, int boardIndex)
        {
            EntityName = name;
            Index = index;
            CurrentHealth = health;
            CurrentAttackPower = attackPower;
            BoardIndex = boardIndex;
        }
        
        /// <summary>
        /// Returns true if entity dies.
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public bool TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            Debug.Log($"{EntityName} took {damage} damage. Remaining health: {CurrentHealth}");
            GameLogicEventManager.BroadcastEntityDamaged(BoardIndex, damage, CurrentHealth)?.Invoke();
            
            if (IsAlive) return false;
            
            Die();
            return true;
        }

        private void Die()
        {
            Debug.Log($"{EntityName} has died.");
            GameLogicEventManager.BroadcastEntityDied(BoardIndex)?.Invoke();
            // Die action => Dead animation
        }
    }
}