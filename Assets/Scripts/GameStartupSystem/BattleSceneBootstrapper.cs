using System;
using Data;
using GameLogic;
using GameLogic.Battle;
using GameLogic.Battle.BotStrategy;
using GameLogic.Battle.Combat;
using GameLogic.Battle.Entity;
using GameStartupSystem.Bootstrapper;
using GameStartupSystem.Bootstrapper.Utility;
using UI.Controllers;
using UnityEngine;

namespace GameStartupSystem
{
    public class BattleSceneBootstrapper: SceneBootstrapper
    {
        [SerializeField] private BotStrategyType botStrategyType = BotStrategyType.Random;
        [SerializeField] private bool specifiedSeedEnabled;
        [SerializeField] private int randomSeed;
        [SerializeField] private GameObject battleCanvasPrefab;
        [SerializeField] private GameObject heroPrefab;
        [SerializeField] private GameObject enemyPrefab;
        
        public override void Initialize()
        {
            if (battleCanvasPrefab == null) return;
            
            // Dependencies
            var gameStateManager = ServiceLocator.Resolve<GameStateManager>();
            var entityService = ServiceLocator.Resolve<EntityService>();
            
            var gameState = gameStateManager.GetGameStateUI();
            var entityFactory = new BattleEntityFactory(heroPrefab, enemyPrefab);
            new BattleManager(new AttackHandler(), gameState, entityService, new EntitySpawner(entityFactory), InitializeBotStrategy(botStrategyType));

            var battleSceneController = Instantiate(battleCanvasPrefab, transform).GetComponent<BattleSceneController>();
            battleSceneController.Initialize(gameState, entityService);
        }
        private IBotStrategy InitializeBotStrategy(BotStrategyType type)
        {
            return type switch
            {
                BotStrategyType.Random => specifiedSeedEnabled ? new RandomBotStrategy(randomSeed) : new RandomBotStrategy(),
                BotStrategyType.Easy => new EasyBotStrategy(),
                BotStrategyType.Normal => new NormalBotStrategy(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        protected override void OnDestroy()
        {
            // Cleanup Behavior
        }
    }
}