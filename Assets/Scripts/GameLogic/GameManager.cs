using System;
using System.Collections.Generic;
using Core;
using Core.EventManager.GameLogicEventManager;
using UnityEngine;

namespace GameLogic
{
    public class GameManager
    {
        private readonly UserDataManager _userDataManager;
        private readonly SceneTransitionService _sceneTransitionService;
        
        public GameManager(UserDataManager userDataManager, SceneTransitionService sceneTransitionService)
        {
            _userDataManager = userDataManager;
            _sceneTransitionService = sceneTransitionService;

            SubscribeEvents();
        }
        
        public void Cleanup()
        {
            UnSubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            GameLogicEventManager.OnHeroesUpdateRequested += UpdateSelectedHeroes;
            GameLogicEventManager.OnBattleStartRequested += HandleBattleStart;
            GameLogicEventManager.OnBattleComplete += HandleBattleComplete;
        }

        private void UnSubscribeEvents()
        {
            GameLogicEventManager.OnHeroesUpdateRequested -= UpdateSelectedHeroes;
            GameLogicEventManager.OnBattleStartRequested -= HandleBattleStart;
            GameLogicEventManager.OnBattleComplete -= HandleBattleComplete;
        }
        
        private void UpdateSelectedHeroes(List<int> heroIndexes)
        {
            _userDataManager.UpdateSelectedHeroes(heroIndexes);
        }

        private async void HandleBattleStart()
        {
            try
            {
                var selectedHeroes = _userDataManager.SelectedHeroIndexes;
                if (selectedHeroes.Count != Constants.MaxSelectedHeroes) return;
                
                await _sceneTransitionService.LoadSceneAsync(Constants.BattleSceneIndex/*, postLoad: GameLogicEventManager.BroadcastBattleSceneLoaded*/);
                Debug.Log("Battle Scene Transition Complete!");
                GameLogicEventManager.BroadcastBattleSceneLoaded?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError($"StartBattle failed! Exception: {e.Message}");
            }
        }
        
        private async void HandleBattleComplete(bool victory, List<int> aliveHeroIndexes)
        {
            try
            {
                if (victory) HandleBattleWin(aliveHeroIndexes);

                CheckHeroUnlock();
                await _sceneTransitionService.LoadSceneAsync(Constants.HeroSelectionSceneIndex/*, postLoad: GameLogicEventManager.BroadcastHeroSelectionSceneLoaded*/);
                Debug.Log("HeroSelection Scene Transition Complete!");
            }
            catch (Exception e)
            {
                Debug.LogError($"FinishBattle failed! Exception: {e.Message}");
            }
        }

        private void HandleBattleWin(List<int> aliveHeroIndexes)
        {
            if(aliveHeroIndexes == null || aliveHeroIndexes.Count == 0) return;
            
            var count = aliveHeroIndexes.Count;
            for (var i = 0; i < count; i++)
            {
                var heroIndex = aliveHeroIndexes[i];
                // TODO: Update Hero Exp
            }
            var newLevel = _userDataManager.CurrentLevel + 1;
            _userDataManager.UpdateLevel(newLevel);
        }

        private void CheckHeroUnlock()
        {
            // TODO: Check Hero Unlock, if unlocked heroes < 10 , then unlock a new hero on every 5th game.
        }
    }
}