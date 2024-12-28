using Data.ScriptableObjects;
using Visual.Rendering.DamageValue;

namespace Visual.Rendering.EntityRenderer
{
    public class BattleEnemyRenderer: BattleEntityRenderer
    {
        public void Initialize(EntitySO entitySo, int boardIndex, DamageValueSpawner damageValueSpawner)
        {
            base.Initialize(entitySo.EntityName, entitySo.Color, entitySo.BaseHealth, boardIndex, damageValueSpawner);
        }
    }
}