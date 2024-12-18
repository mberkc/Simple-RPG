using Core;
using Core.BootStrapper;
using Data;
using Data.ScriptableObjects;
using GameLogic;
using GameLogic.State;
using UnityEngine;

namespace BootStrappers
{
    public class MainBootStrapper : GameBootStrapper
    {
        
        [SerializeField] private HeroData[] heroes;
        [SerializeField] private EnemyData[] enemies; 
        private StateManager stateManager;
        private GameFlowManager gameFlowManager;
        
        public override void Initialize()
        {
            Debug.Log("Initializing Main BootStrapper!");
            var heroAmount = heroes.Length;
            var enemyAmount = enemies.Length;
            Debug.Log($"Available Heroes: {heroAmount}");
            Debug.Log($"Available Enemies: {enemyAmount}");
            if (heroAmount != Constants.TotalHeroes || enemyAmount != Constants.TotalEnemies)
            {
                Debug.LogError("Please assign all Heroes and Enemies to Main BootStrapper!");
                return;
            }
            
            EntityDatabase.Initialize(heroes, enemies);
            
            stateManager = new StateManager();
            stateManager.Initialize();
            
            gameFlowManager = new GameFlowManager();
            gameFlowManager.Initialize();
        }
    }
}