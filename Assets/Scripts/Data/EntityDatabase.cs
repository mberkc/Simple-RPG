using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Data.ScriptableObjects;
using UnityEngine;

[assembly: InternalsVisibleTo("BootStrappers")]
namespace Data
{
    public static class EntityDatabase
    {
        // ConcurrentDictionary can be used for thread-safety
        private static Dictionary<string, HeroData> _heroes = new Dictionary<string, HeroData>();
        private static Dictionary<string, EnemyData> _enemies = new Dictionary<string, EnemyData>();

        private static HeroData[] _orderedHeroArray;
        private static EnemyData[] _orderedEnemyArray;

        // Initialize the database with hero and enemy data
        internal static void Initialize(HeroData[] heroArray, EnemyData[] enemyArray)
        {
            foreach (var hero in heroArray)
                _heroes[hero.Id] = hero;

            foreach (var enemy in enemyArray)
                _enemies[enemy.Id] = enemy;
            
            // If arrays are already sorted delete these.
            _orderedHeroArray = heroArray.OrderBy(hero => hero.Index).ToArray();
            _orderedEnemyArray = enemyArray.OrderBy(enemy => enemy.Index).ToArray();
        }
        
        internal static void AddHero(HeroData hero)
        {
            var id = hero.Id;
            if (_heroes.TryAdd(id, hero)) return;
                
            Debug.LogWarning($"Hero with ID {id} already exists.");
        }
        
        internal static void RemoveHero(string id)
        {
            if (_heroes.Remove(id)) return;
            
            Debug.LogWarning($"Hero with ID {id} does not exist.");
        }

        internal static void AddEnemy(EnemyData enemy)
        {
            var id = enemy.Id;
            if (_enemies.TryAdd(id, enemy)) return;
            
            Debug.LogWarning($"Enemy with ID {id} already exists.");
        }

        internal static void RemoveEnemy(string id)
        {
            if (_enemies.Remove(id)) return;
            
            Debug.LogWarning($"Enemy with ID {id} does not exist.");
        }

        public static HeroData GetHeroById(string id)
        {
            if (_heroes.TryGetValue(id, out var hero))
                return hero;

            Debug.LogError($"Hero with ID {id} not found.");
            return null;
        }

        public static EnemyData GetEnemyById(string id)
        {
            if (_enemies.TryGetValue(id, out var enemy))
                return enemy;

            Debug.LogError($"Enemy with ID {id} not found.");
            return null;
        }
        
        public static HeroData GetHeroByIndex(int index)
        {
            var hero = _orderedHeroArray[index];
            if (hero == null) 
                Debug.LogError($"Hero with Index {index} not found.");
            return hero;
        }

        public static EnemyData GetEnemyByIndex(int index)
        {
            var enemy = _orderedEnemyArray[index];
            if (enemy == null) 
                Debug.LogError($"Enemy with Index {index} not found.");
            return enemy;
        }
    }
}