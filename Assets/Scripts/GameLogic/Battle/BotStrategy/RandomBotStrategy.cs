using System;
using System.Linq;
using System.Threading.Tasks;
using GameLogic.Battle.Entity;

namespace GameLogic.Battle.BotStrategy
{
    public class RandomBotStrategy : IBotStrategy
    {
        private readonly Random _random;
        private readonly Random _waitTimeRandom = new (); // initializing without seed, if it's important gamedesign-wise then it can be changed.
        
        public RandomBotStrategy()
        {
            _random = new Random();
        }

        public RandomBotStrategy(int seed)
        {
            _random = new Random(seed);
        }

        public async Task<BattleEntity> ChooseTarget(BattleEntity[] heroes)
        {
            var randomWaitMS = _waitTimeRandom.Next(500, 2000);
            await Task.Delay(randomWaitMS);
            var aliveHeroes = heroes.Where(hero => hero.IsAlive).ToArray();
            if (aliveHeroes.Length == 0) return null;

            var randomIndex = _random.Next(aliveHeroes.Length);
            return aliveHeroes[randomIndex];
        }
    }
}