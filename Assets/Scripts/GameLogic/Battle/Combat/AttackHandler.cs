using System.Threading.Tasks;
using Core;
using Core.EventManager.GameLogicEventManager;
using GameLogic.Battle.Entity;
using UnityEngine;

namespace GameLogic.Battle.Combat
{
    public class AttackHandler
    {
        /// <summary>
        /// Returns true if attack is successful
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteAttack(BattleEntity attacker, BattleEntity target)
        {
            if (attacker is not { IsAlive: true } || target is not { IsAlive: true })
            {
                Debug.LogWarning("Invalid attack: One or both entities are dead.");
                return false;
            }
            
            GameLogicEventManager.BroadcastEntityAttacked(attacker.BoardIndex)?.Invoke();
            Debug.Log($"{attacker.EntityName} is attacking {target.EntityName} for {attacker.CurrentAttackPower} damage.");

            await Task.Delay(CoreUtility.GetNormalAnimationDurationAsMS); // Attack wait

            await target.TakeDamage(attacker.CurrentAttackPower); // Damage taken (&die) wait
            
            return true;
        }
    }
}