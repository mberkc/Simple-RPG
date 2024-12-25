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
        
        
        public void Cleanup() // Call on Parent MonoBehaviour OnDestroy if needed (currently GameManager exists during game).
        {
            UnSubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            GameLogicEventManager.OnBattleStartRequested += HandleBattleStart;
            GameLogicEventManager.OnReturnToHeroSelectionRequested += ReturnToHeroSelection;
            GameLogicEventManager.OnHeroesUpdateRequested += UpdateSelectedHeroes;
            GameLogicEventManager.OnBattleComplete += HandleBattleComplete;
        }

        private void UnSubscribeEvents()
        {
            GameLogicEventManager.OnBattleStartRequested -= HandleBattleStart;
            GameLogicEventManager.OnReturnToHeroSelectionRequested -= ReturnToHeroSelection;
            GameLogicEventManager.OnHeroesUpdateRequested -= UpdateSelectedHeroes;
            GameLogicEventManager.OnBattleComplete -= HandleBattleComplete;
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
                Debug.LogError($"Battle Start failed! Exception: {e.Message}");
            }
        }
        
        private async void ReturnToHeroSelection()
        {
            try
            {
                await _sceneTransitionService.LoadSceneAsync(Constants.HeroSelectionSceneIndex/*, postLoad: GameLogicEventManager.BroadcastHeroSelectionSceneLoaded*/);
                Debug.Log("HeroSelection Scene Transition Complete!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Return to HeroSelection Scene failed! Exception: {e.Message}");
            }
        }
        
        private void UpdateSelectedHeroes(List<int> heroIndexes)
        {
            _userDataManager.UpdateSelectedHeroes(heroIndexes);
        }
        
        private void HandleBattleComplete(bool victory, List<int> aliveHeroIndexes)
        {
            if (victory) HandleBattleWin(aliveHeroIndexes);

            CheckHeroUnlock();
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