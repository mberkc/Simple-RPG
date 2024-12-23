using System;
using System.Linq;
using System.Threading.Tasks;
using GameLogic.Battle.Entity;

namespace GameLogic.Battle.BotStrategy
{
    public class EasyBotStrategy : IBotStrategy
    {
        private readonly Random _waitTimeRandom = new ();

        public async Task<BattleEntity> ChooseTarget(BattleEntity[] heroes)
        {
            var randomWaitMS = _waitTimeRandom.Next(500, 1500);
            await Task.Delay(randomWaitMS);
            return heroes
                .Where(hero => hero.IsAlive)
                .OrderByDescending(hero => hero.CurrentHealth)
                .FirstOrDefault();
        }
    }
}