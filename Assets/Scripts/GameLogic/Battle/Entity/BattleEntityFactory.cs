using Data;
using Data.ScriptableObjects;

namespace GameLogic.Battle.Entity
{
    public class BattleEntityFactory
    {
        public BattleEntity CreateHero(HeroData heroData, int boardIndex)
        {
            return new BattleEntity(
                heroData.EntityName, 
                heroData.Index,
                heroData.ModifiedHealth,
                heroData.ModifiedAttackPower,
                boardIndex);
        }

        public BattleEntity CreateEnemy(EnemySO enemySo, int boardIndex)
        {
            return new BattleEntity(
                enemySo.EntityName, 
                enemySo.Index,
                enemySo.BaseHealth,
                enemySo.BaseAttackPower,
                boardIndex);
        }
    }
}