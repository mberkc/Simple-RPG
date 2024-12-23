using System.Threading.Tasks;
using GameLogic.Battle.Entity;

namespace GameLogic.Battle.BotStrategy
{
    public interface IBotStrategy
    {
        Task<BattleEntity> ChooseTarget(BattleEntity[] heroes);
    }
}