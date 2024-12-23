using System;
using System.Linq;
using System.Threading.Tasks;
using GameLogic.Battle.Entity;

namespace GameLogic.Battle.BotStrategy
{
    public class NormalBotStrategy : IBotStrategy
    {
        private readonly Random _waitTimeRandom = new ();
        
        public async Task<BattleEntity> ChooseTarget(BattleEntity[] heroes)
        {
            var randomWaitMS = _waitTimeRandom.Next(1000, 2500);
            await Task.Delay(randomWaitMS);
            return heroes
                .Where(hero => hero.IsAlive)
                .OrderBy(hero => hero.CurrentHealth)
                .FirstOrDefault();
        }
    }
}