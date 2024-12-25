using Core.EventManager.GameLogicEventManager;
using GameLogic.Battle.Entity;
using UnityEngine;

namespace GameLogic.Battle.Combat
{
    public class AttackHandler
    {
        public bool ExecuteAttack(BattleEntity attacker, BattleEntity target)
        {
            if (attacker is not { IsAlive: true } || target is not { IsAlive: true })
            {
                Debug.LogWarning("Invalid attack: One or both entities are dead.");
                return false;
            }
            
            GameLogicEventManager.BroadcastEntityAttacked(attacker.BoardIndex)?.Invoke();
            Debug.Log($"{attacker.EntityName} is attacking {target.EntityName} for {attacker.CurrentAttackPower} damage.");
            
            if (target.TakeDamage(attacker.CurrentAttackPower))
                Debug.Log($"{target.EntityName} has been defeated!");
            return true;
        }
    }
}