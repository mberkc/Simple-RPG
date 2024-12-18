using Core;
using Core.EventManager.GameLogicEventManager;
using Core.Initializable;


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
            var heroes = GameState.SelectedHeroes;
            if(heroes is not { Length: Constants.MaxSelectedHeroes }) return;
                
            // Update GameState with enemy to face
            GameState.EnemyToFace = EnemySelector.GetEnemyForLevel(LevelManager.CurrentLevel);
            
            // Load the Battle Scene
            await SceneLoader.LoadBattleSceneAsync();
            GameLogicEventManager.BroadcastBattleSceneLoaded?.Invoke();
        }
    }
}