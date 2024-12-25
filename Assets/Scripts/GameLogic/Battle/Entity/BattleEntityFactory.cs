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
                heroData.Health,
                heroData.AttackPower,
                boardIndex);
        }

        public BattleEntity CreateEnemy(EnemyData enemyData, int boardIndex)
        {
            return new BattleEntity(
                enemyData.EntityName, 
                enemyData.Index,
                enemyData.Health,
                enemyData.AttackPower,
                boardIndex);
        }
    }
}