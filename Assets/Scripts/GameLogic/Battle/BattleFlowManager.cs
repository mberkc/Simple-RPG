using System;
using Core.EventManager.GameLogicEventManager;
using Data;
using GameLogic.Battle.BotStrategy;
using GameLogic.Battle.Entity;
using UnityEngine;

namespace GameLogic.Battle
{
    public class BattleFlowManager
    {
        private readonly CombatSystem _combatSystem;
        private readonly PlayerManager _playerManager;
        private readonly OpponentManager _opponentManager;
        private BattleState _currentState = BattleState.Idle;
        
        public BattleFlowManager(BattleEntityFactory entityFactory, CombatSystem combatSystem, GameState gameState, EntityService entityService, IBotStrategy botStrategy)
        {
            _combatSystem = combatSystem;
            _playerManager = new PlayerManager(entityFactory, gameState, entityService);
            _opponentManager = new OpponentManager(entityFactory, gameState, entityService, botStrategy);
            ProcessState(BattleState.Initialize);
        }
        
        private void ProcessState(BattleState state)
        {
            _currentState = state;
            switch (_currentState)
            {
                case BattleState.Initialize:
                    HandleInitialize();
                    break;
                case BattleState.PlayerTurn:
                    HandlePlayerTurn();
                    break;
                case BattleState.OpponentTurn:
                    HandleOpponentTurn();
                    break;
                case BattleState.Victory:
                    CompleteBattle(true);
                    break;
                case BattleState.Defeat:
                    CompleteBattle(false);
                    break;
                case BattleState.Idle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleInitialize()
        {
            // Initialize things needed
            Debug.Log("Waiting for battle start!");
            // Subscribe start battle event => BattleStarted()
        }

        private void HandlePlayerTurn()
        {
            Debug.Log("Player turn!");
            // Subscribe player turn finish event => PlayerTurnEnded()
            // Implement logic for player actions
            // Enable Player inputs
        }
        
        private async void HandleOpponentTurn()
        {
            Debug.Log("Opponent turn!");
            // Subscribe opponent turn finish event => OpponentTurnEnded()
            await _opponentManager.HandleOpponentAttack(_playerManager.GetHeroEntities(), _combatSystem);
        }
        
        private void CompleteBattle(bool victory)
        {
            Debug.Log("Battle completed!");
            var aliveHeroIndexes = victory ? _playerManager.GetAliveHeroIndexes() : null;
            GameLogicEventManager.BroadcastBattleComplete(victory, aliveHeroIndexes)?.Invoke();
        }

        #region Event Callbacks

        private void BattleStarted()
        {
            Debug.Log("Battle started!");
            ProcessState(BattleState.PlayerTurn);
        }

        private void PlayerTurnEnded()
        {
            Debug.Log("Player turn ended!");
            ProcessState(_opponentManager.CheckIfEnemyIsDefeated ? BattleState.Victory : BattleState.OpponentTurn);
        }
        
        private void OpponentTurnEnded()
        {
            Debug.Log("Player turn ended!");
            ProcessState(_playerManager.CheckIfAllHeroesAreDefeated ? BattleState.Defeat : BattleState.PlayerTurn);
        }

        #endregion
    }
}

public enum BattleState
{
    Idle,
    Initialize,
    PlayerTurn,
    OpponentTurn,
    Victory,
    Defeat
}