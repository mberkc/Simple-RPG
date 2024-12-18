using Data.ScriptableObjects;
using GameLogic.State;
using UnityEngine;

namespace GameLogic
{
    public class BattleManager : MonoBehaviour
    {
        private HeroData[] heroes;
        private EnemyData enemy;

        private void Awake()
        {
            heroes = GameState.SelectedHeroes;
            enemy = GameState.EnemyToFace;

            for (var i = 0; i < heroes.Length; i++)
            {
                var hero = heroes[i];
                Debug.Log($"Hero {i+1}: {hero.EntityName}, HP: {hero.Health}, AP: {hero.AttackPower}");
                // Instantiate and initialize hero GameObjects here
            }
        }
    }
}