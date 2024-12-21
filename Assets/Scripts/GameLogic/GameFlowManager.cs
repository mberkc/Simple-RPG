using System;
using Core;
using Core.EventManager.GameLogicEventManager;
using UnityEngine;


namespace GameLogic
{
    public class GameFlowManager
    {
        private readonly GameStateManager _gameStateManager;
        private readonly SceneTransitionManager _sceneTransitionManager;
        
        public GameFlowManager(GameStateManager gameStateManager, SceneTransitionManager sceneTransitionManager)
        {
            _gameStateManager = gameStateManager;
            _sceneTransitionManager = sceneTransitionManager;

            SubscribeEvents();
        }
        
        public void Cleanup()
        {
            UnSubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            GameLogicEventManager.OnBattleStartRequested += StartBattle;
        }

        private void UnSubscribeEvents()
        { 
            GameLogicEventManager.OnBattleStartRequested -= StartBattle;
        }
        
        // TODO: Use LevelService & EnemySelector?

        private async void StartBattle()
        {
            try
            {
                var selectedHeroes = _gameStateManager.SelectedHeroIndexes;
                if (selectedHeroes.Count != Constants.MaxSelectedHeroes) return;

                var currentLevel = _gameStateManager.CurrentLevel;
                // TODO Get current enemy index?
                
                await _sceneTransitionManager.LoadSceneAsync(Constants.BattleSceneIndex/*, postLoad: GameLogicEventManager.BroadcastBattleSceneLoaded*/);
            }
            catch (Exception e)
            {
                Debug.LogError($"StartBattle failed! Exception: {e.Message}");
            }
        }
        
        public async void FinishBattle(bool success)
        {
            try
            {
                if (success)
                {
                    HandleBattleWon();
                }
                
                // TODO: Check Hero Unlock
                
                // Load the Hero Selection Scene
                await SceneLoader.LoadHeroSelectionSceneAsync();
                GameLogicEventManager.BroadcastHeroSelectionSceneLoaded?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError($"FinishBattle failed! Exception: {e.Message}");
            }
        }

        private void HandleBattleWon()
        {
            // TODO: Update Hero Exp
            var newLevel = _gameStateManager.CurrentLevel + 1;
            _gameStateManager.UpdateLevel(newLevel);
        }
    }
}