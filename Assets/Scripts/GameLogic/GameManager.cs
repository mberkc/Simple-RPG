using System;
using System.Collections.Generic;
using Core;
using Core.EventManager.GameLogicEventManager;
using GameLogic.Battle;
using UnityEngine;

namespace GameLogic
{
    public class GameManager
    {
        private readonly UserDataManager _userDataManager;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly HeroProgressionService _heroProgressionService;
        
        public GameManager(UserDataManager userDataManager, SceneTransitionService sceneTransitionService, HeroProgressionService heroProgressionService)
        {
            _userDataManager = userDataManager;
            _sceneTransitionService = sceneTransitionService;
            _heroProgressionService = heroProgressionService;
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
            var level = _userDataManager.CurrentLevel;
            var playAmount = _userDataManager.BattlePlayAmount + 1;
            if (victory) ++level;

            _userDataManager.UpdateLevelAndPlayAmount(level, playAmount);
            _heroProgressionService.CheckHeroProgression(victory, aliveHeroIndexes, playAmount);
            _userDataManager.SaveAllChangesAsync(); // Task can be awaited if needed!
        }
    }
}