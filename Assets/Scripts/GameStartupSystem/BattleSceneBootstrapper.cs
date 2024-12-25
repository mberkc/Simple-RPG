using System;
using Data;
using GameLogic;
using GameLogic.Battle;
using GameLogic.Battle.BotStrategy;
using GameLogic.Battle.Combat;
using GameLogic.Battle.Entity;
using GameStartupSystem.Bootstrapper;
using GameStartupSystem.Bootstrapper.Utility;
using UnityEngine;
using Visual.Controllers;
using Visual.Rendering;

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
        
        private BattleManager battleManager;
        
        public override void Initialize()
        {
            if (battleCanvasPrefab == null) return;
            
            // Dependencies
            var userDataManager = ServiceLocator.Resolve<UserDataManager>();
            var entityService = ServiceLocator.Resolve<EntityService>();

            var attackHandler = new AttackHandler();
            var entitySpawner = new EntitySpawner(new BattleEntityFactory());
            battleManager = new BattleManager(attackHandler, userDataManager.GetUserData(), entityService, entitySpawner, InitializeBotStrategy(botStrategyType));

            
            var battleSceneController = Instantiate(battleCanvasPrefab, transform).GetComponent<BattleSceneController>();
            battleSceneController.Initialize(userDataManager.GetUserDataUI(), entityService, new EntityRendererFactory(heroPrefab, enemyPrefab));
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
            battleManager.Cleanup();
        }
    }
}