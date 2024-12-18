using Core.Initializable;
using Data.ScriptableObjects;
using EventManager.GameLogicEventManager;
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

        private async void StartBattle()
        {
            HeroData[] selectedHeroes = null;
            EnemyData enemyToFace = null;
            // Update GameState with selected heroes & enemy to face
            GameState.SelectedHeroes = selectedHeroes;
            GameState.EnemyToFace = enemyToFace;
            // Load the Battle Scene
            await SceneLoader.LoadBattleSceneAsync();
            Debug.Log("Load Task Completed");
            GameLogicEventManager.BroadcastBattleSceneLoaded?.Invoke();
        }
    }
}