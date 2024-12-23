using GameLogic.Battle.Entity;
using UnityEngine;

namespace GameLogic.Battle
{
    public class CombatSystem
    {
        public void ExecuteAttack(BattleEntity attacker, BattleEntity target)
        {
            if (!attacker.IsAlive || !target.IsAlive)
            {
                Debug.LogWarning("Invalid attack: One or both entities are dead.");
                return;
            }

            Debug.Log($"{attacker.EntityName} is attacking {target.EntityName} for {attacker.CurrentAttackPower} damage.");
            // Take damage action => UI tween animation
            
            if (target.TakeDamage(attacker.CurrentAttackPower))
            {
                Debug.Log($"{target.EntityName} has been defeated!");
            }
        }
    }
}