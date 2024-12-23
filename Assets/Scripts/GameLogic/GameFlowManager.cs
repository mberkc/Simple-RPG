using System;
using System.Collections.Generic;
using Core;
using Core.EventManager.GameLogicEventManager;
using UnityEngine;

namespace GameLogic
{
    public class GameFlowManager
    {
        private readonly GameStateManager _gameStateManager;
        private readonly SceneTransitionService _sceneTransitionService;
        
        public GameFlowManager(GameStateManager gameStateManager, SceneTransitionService sceneTransitionService)
        {
            _gameStateManager = gameStateManager;
            _sceneTransitionService = sceneTransitionService;

            SubscribeEvents();
        }
        
        public void Cleanup()
        {
            UnSubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            GameLogicEventManager.OnBattleStartRequested += HandleBattleStart;
            GameLogicEventManager.OnBattleComplete += HandleBattleComplete;
        }

        private void UnSubscribeEvents()
        {
            GameLogicEventManager.OnBattleStartRequested -= HandleBattleStart;
            GameLogicEventManager.OnBattleComplete -= HandleBattleComplete;
        }
        
        // TODO: Use LevelService & EnemySelector?

        private async void HandleBattleStart()
        {
            try
            {
                var selectedHeroes = _gameStateManager.SelectedHeroIndexes;
                if (selectedHeroes.Count != Constants.MaxSelectedHeroes) return;

                var currentLevel = _gameStateManager.CurrentLevel;
                // TODO Get current enemy index?
                
                await _sceneTransitionService.LoadSceneAsync(Constants.BattleSceneIndex/*, postLoad: GameLogicEventManager.BroadcastBattleSceneLoaded*/);
            }
            catch (Exception e)
            {
                Debug.LogError($"StartBattle failed! Exception: {e.Message}");
            }
        }
        
        private async void HandleBattleComplete(bool success, List<int> aliveHeroIndexes)
        {
            try
            {
                if (success)
                    HandleBattleWin(aliveHeroIndexes);

                CheckHeroUnlock();
                
                await _sceneTransitionService.LoadSceneAsync(Constants.HeroSelectionSceneIndex/*, postLoad: GameLogicEventManager.BroadcastHeroSelectionSceneLoaded*/);
            }
            catch (Exception e)
            {
                Debug.LogError($"FinishBattle failed! Exception: {e.Message}");
            }
        }

        private void HandleBattleWin(List<int> aliveHeroIndexes)
        {
            var count = aliveHeroIndexes.Count;
            for (var i = 0; i < count; i++)
            {
                var heroIndex = aliveHeroIndexes[i];
                // TODO: Update Hero Exp
            }
            var newLevel = _gameStateManager.CurrentLevel + 1;
            _gameStateManager.UpdateLevel(newLevel);
        }

        private void CheckHeroUnlock()
        {
            // TODO: Check Hero Unlock, if unlocked heroes < 10 , then unlock a new hero on every 5th game.
        }
    }
}