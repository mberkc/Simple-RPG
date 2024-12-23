using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Progression;
using Data;

namespace GameLogic
{
    /// <summary>
    /// Manipulates GameState and saves/loads via Progression Service
    /// </summary>
    public class GameStateManager
    {
        private readonly ProgressionService _progressionService;
        private readonly GameState _gameState;
        //private readonly GameStateUI _gameStateUI;
        
        private ProgressionData progressionData;

        public GameStateManager(GameState gameState, ProgressionService progressionService)
        {
            _gameState = gameState;
            //_gameStateUI = gameStateUI;
            _progressionService = progressionService;
        }
        
        public int CurrentLevel => _gameState.CurrentLevel;
        public List<int> SelectedHeroIndexes => _gameState.SelectedHeroIndexes;

        public async Task InitializeGameStateAsync()
        {
            progressionData = await _progressionService.LoadProgressionAsync();
            _gameState.CurrentLevel = progressionData.CurrentLevel;
            _gameState.SelectedHeroIndexes = progressionData.SelectedHeroIndexes;
        }

        public void UpdateLevel(int level)
        {
            _gameState.CurrentLevel = level;
            SaveGameStateAsync();
        }

        public void UpdateSelectedHeroes(List<int> heroIndexes)
        {
            _gameState.SelectedHeroIndexes = heroIndexes;
            SaveGameStateAsync();
        }

        private async Task SaveGameStateAsync()
        {
            progressionData.CurrentLevel = _gameState.CurrentLevel;
            progressionData.SelectedHeroIndexes = _gameState.SelectedHeroIndexes;
            await _progressionService.SaveProgressionAsync(progressionData);
        }
        
        // Extracts GameState for DI
        public GameState GetGameStateUI()
        {
            return _gameState;
        }
    }
}