using System;
using Core;
using Core.EventManager.GameLogicEventManager;
using Core.Initializable;
using GameLogic.State;
using UnityEngine;


namespace GameLogic
{
    public class GameFlowManager : Initializable
    {
        protected override void SubscribeEvents()
        {
            GameLogicEventManager.OnBattleStartRequested += StartBattle;
        }

        protected override void UnSubscribeEvents()
        { 
            GameLogicEventManager.OnBattleStartRequested += StartBattle;
        }
        
        // TODO: Use LevelService & EnemySelector?

        private async void StartBattle()
        {
            try
            {
                var heroes = GameState.SelectedHeroes;
                if(heroes is not { Length: Constants.MaxSelectedHeroes }) return;

                var currentLevel = progressionService.GetCurrentLevel();
                GameState.CurrentLevel = currentLevel;
                GameState.EnemyToFace = EnemySelector.GetEnemyForLevel(currentLevel);
            
                // Load the Battle Scene
                await SceneLoader.LoadBattleSceneAsync();
                GameLogicEventManager.BroadcastBattleSceneLoaded?.Invoke();
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
                    // TODO: Update Hero Exp
                    progressionService.SaveCurrentLevel(++GameState.CurrentLevel);
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
    }
}