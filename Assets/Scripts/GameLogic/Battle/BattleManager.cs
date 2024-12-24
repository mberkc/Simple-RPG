using System;
using Core.EventManager.GameLogicEventManager;
using Data;
using GameLogic.Battle.BotStrategy;
using GameLogic.Battle.Combat;
using GameLogic.Battle.Entity;
using UnityEngine;

namespace GameLogic.Battle
{
    public class BattleManager
    {
        private enum BattleState
        {
            Idle,
            Initialize,
            PlayerTurn,
            OpponentTurn,
            Victory,
            Defeat
        }
        
        private readonly CombatController _combatController;
        private readonly PlayerManager _playerManager;
        private readonly OpponentManager _opponentManager;
        private BattleState currentState = BattleState.Idle;
        
        public BattleManager(AttackHandler attackHandler, UserData userData, EntityService entityService, EntitySpawner entitySpawner, IBotStrategy botStrategy)
        {
            _playerManager = new PlayerManager(userData, entityService, entitySpawner);
            _opponentManager = new OpponentManager(userData, entityService, entitySpawner, botStrategy);
            _combatController = new CombatController(attackHandler, _playerManager, _opponentManager);
            SubscribeEvents();
            ProcessState(BattleState.Initialize);
        }
        
        public void Cleanup()
        {
            UnSubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            GameLogicEventManager.OnBattleSceneLoaded += BattleSceneLoaded;
            GameLogicEventManager.OnPlayerAttackRequested += HandlePlayerAttack;
            _combatController.OnPlayerAttackFinished += PlayerTurnEnded;
            _combatController.OnOpponentAttackFinished += OpponentTurnEnded;
        }

        private void UnSubscribeEvents()
        {
            GameLogicEventManager.OnBattleSceneLoaded -= BattleSceneLoaded;
            GameLogicEventManager.OnPlayerAttackRequested -= HandlePlayerAttack;
            _combatController.OnPlayerAttackFinished -= PlayerTurnEnded;
            _combatController.OnOpponentAttackFinished -= OpponentTurnEnded;
        }
        
        private void ProcessState(BattleState state)
        {
            currentState = state;
            switch (currentState)
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
        }

        private void HandlePlayerTurn()
        {
            Debug.Log("Player turn!");
            // Enable Player inputs
        }
        
        private void HandleOpponentTurn()
        {
            Debug.Log("Opponent turn!");
            _combatController.HandleOpponentAttack();
        }
        
        private void CompleteBattle(bool victory)
        {
            Debug.Log("Battle completed!");
            var aliveHeroIndexes = victory ? _playerManager.GetAliveHeroIndexes : null;
            GameLogicEventManager.BroadcastBattleComplete(victory, aliveHeroIndexes)?.Invoke();
        }

        private void StartBattle()
        {
            Debug.Log("Battle started!");
            ProcessState(BattleState.PlayerTurn);
        }

        #region Event Callbacks

        private void BattleSceneLoaded()
        {
            if(currentState != BattleState.Initialize) return;

            StartBattle();
        }
        
        private void HandlePlayerAttack(int attackerIndex)
        {
            _combatController.HandlePlayerAttack(attackerIndex);
        }

        private void PlayerTurnEnded()
        {
            Debug.Log("Player turn ended!");
            GameLogicEventManager.BroadcastPlayerTurnEnded?.Invoke();
            ProcessState(_opponentManager.CheckIfEnemyIsDefeated ? BattleState.Victory : BattleState.OpponentTurn);
        }
        
        private void OpponentTurnEnded()
        {
            Debug.Log("Player turn ended!");
            GameLogicEventManager.BroadcastOpponentTurnEnded?.Invoke();
            ProcessState(_playerManager.CheckIfAllHeroesAreDefeated ? BattleState.Defeat : BattleState.PlayerTurn);
        }

        #endregion
    }
}
