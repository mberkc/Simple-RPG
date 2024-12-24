using Data.ScriptableObjects;
using UnityEngine;

namespace GameLogic.Battle.Entity
{
    /// <summary>
    /// Obsolete Class
    /// </summary>
    public class HeroManager : MonoBehaviour
    {
        private HeroData heroData;

        public void Initialize(HeroData hero)
        {
            heroData = hero;
            Debug.Log($"Hero Initialized: {heroData.EntityName}, HP: {heroData.Health}, AP: {hero.AttackPower}");
        }
    }
}