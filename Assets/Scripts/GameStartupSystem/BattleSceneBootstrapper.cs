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
using Visual.Rendering.DamageValue;
using Visual.Rendering.EntityRenderer;

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
        [SerializeField] private GameObject damageValueSpawnerPrefab;
        [SerializeField] private GameObject pointerHandlerUtilityPrefab;
        
        private BattleManager battleManager;
        
        public override void Initialize()
        {
            if (battleCanvasPrefab == null) return;
            
            // Dependencies
            var userDataManager = ServiceLocator.Resolve<UserDataManager>();
            var enemyService = ServiceLocator.Resolve<EnemyService>();

            var attackHandler = new AttackHandler();
            var entitySpawner = new BattleEntitySpawner(new BattleEntityFactory());
            battleManager = new BattleManager(attackHandler, userDataManager.GetUserData(), enemyService, entitySpawner, InitializeBotStrategy(botStrategyType));

            Instantiate(pointerHandlerUtilityPrefab, transform);
            var damageValueSpawner = Instantiate(damageValueSpawnerPrefab, transform).GetComponent<DamageValueSpawner>();
            
            var battleSceneController = Instantiate(battleCanvasPrefab, transform).GetComponent<BattleSceneController>();
            battleSceneController.Initialize(userDataManager.GetUserDataVisual(), enemyService, new EntityRendererFactory(heroPrefab, enemyPrefab, damageValueSpawner));
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