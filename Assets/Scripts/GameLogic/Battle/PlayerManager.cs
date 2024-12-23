using Data.ScriptableObjects;
using UnityEngine;

namespace GameLogic.Battle
{
    /// <summary>
    /// Manages player-controlled heroes.
    /// </summary>
    public class PlayerManager
    {
        private readonly HeroData[] _heroes;

        public PlayerManager(HeroData[] heroes)
        {
            _heroes = heroes;
        }

        public HeroData[] GetHeroes() => _heroes;

        public void ApplyDamage(int heroIndex, float damage)
        {
            var hero = _heroes[heroIndex];
            //hero.Health -= damage;
            Debug.Log($"{hero.EntityName} took {damage} damage!");
        }
    }
}