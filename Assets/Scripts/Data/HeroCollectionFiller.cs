using System.Linq;
using System.Threading.Tasks;
using Data.ScriptableObjects;

namespace Data
{
    public class HeroCollectionFiller
    {
        private readonly HeroCollection _heroCollection;
        
        public HeroCollectionFiller(HeroSO[] heroes)
        {
            _heroCollection = new HeroCollection();
            var orderedHeroes = heroes.OrderBy(h => h.Index).ToArray();
            for (var i = 0; i < orderedHeroes.Length; i++)
            {
                var scriptableHero = orderedHeroes[i];
                var heroData = _heroCollection.Heroes[i];

                heroData.Index = scriptableHero.Index;
                heroData.EntityName = scriptableHero.EntityName;
                heroData.Color = scriptableHero.Color;
                heroData.BaseHealth = scriptableHero.BaseHealth;
                heroData.BaseAttackPower = scriptableHero.BaseAttackPower;

                // Optionally, initialize modified values to base values if needed
                heroData.ModifiedHealth = heroData.BaseHealth;
                heroData.ModifiedAttackPower = heroData.BaseAttackPower;
            }
        }

        public async Task<HeroCollection> HeroCollection()
        {
            await Task.Delay(50);
            return _heroCollection;
        }
    }
}