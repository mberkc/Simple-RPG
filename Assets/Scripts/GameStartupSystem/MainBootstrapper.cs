using System;
using Core;
using Core.Encryption;
using Core.Progression;
using Core.Progression.ProgressionStorage;
using Core.Serialization;
using Data;
using Data.ScriptableObjects;
using GameLogic;
using GameLogic.Battle;
using GameStartupSystem.Bootstrapper;
using GameStartupSystem.Bootstrapper.Utility;
using UnityEngine;

namespace GameStartupSystem
{
    public class MainBootstrapper : GameBootstrapper
    {
        [SerializeField] private int TargetFrameRate = 120;
        [SerializeField] private StorageType storageType = StorageType.Local;
        [SerializeField] private EncryptionType encryptionType = EncryptionType.AES;
        [SerializeField] private SerializationType serializationType = SerializationType.JSON;
        [SerializeField] private string encryptionKey = "0e2tk8M7nbH1pS5z"; // Must be 16 characters for AES

        [SerializeField] private HeroSO[] heroes;
        [SerializeField] private EnemySO[] enemies;
        
        public override async void Initialize()
        {
            try
            {
                Debug.Log("Initializing Main Bootstrapper!");
                Application.targetFrameRate = TargetFrameRate;

                var sceneTransitionService = new SceneTransitionService();
                var enemyService = new EnemyService(enemies, out var maxLevel);
                var heroCollectionFiller = new HeroCollectionFiller(heroes);
                var heroCollection = await heroCollectionFiller.GetHeroCollection();
                var userData = new UserData(heroCollection);
                var userDataManager = new UserDataManager(userData, InitializeProgressionService(), maxLevel);
                await userDataManager.InitializeUserDataAsync();
                
                var heroProgressionService = new HeroProgressionService(userDataManager);
                new GameManager(userDataManager, sceneTransitionService, heroProgressionService, maxLevel);

                RegisterServices(userDataManager, enemyService);
                
                InitializationCompletionSource.TrySetResult(true);
                Debug.Log("Main Bootstrapper initialized!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Main Bootstrapper initialization failed! Exception: {e.Message}");
                InitializationCompletionSource.TrySetException(e);
            }
        }
        
        private void RegisterServices(UserDataManager userDataManager, EnemyService enemyService)
        {
            ServiceLocator.Register(userDataManager);
            ServiceLocator.Register(enemyService);
        }

        #region Progression Service

        private ProgressionService InitializeProgressionService()
        {
            var encryptionService = InitializeEncryptionService(encryptionType);
            var serializationService = InitializeSerializationService(serializationType);
            var progressionStorage = InitializeProgressionStorage(storageType, encryptionService, serializationService);
            var progressionService = new ProgressionService(progressionStorage);
            return progressionService;
        }
        
        private IEncryptionService InitializeEncryptionService(EncryptionType type)
        {
            return type switch
            {
                EncryptionType.AES => new AESEncryptionService(encryptionKey),
                EncryptionType.None => new NoEncryptionService(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
        
        private ISerializationService InitializeSerializationService(SerializationType type)
        {
            return type switch
            {
                SerializationType.JSON => new JsonSerializationService(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private IProgressionStorage InitializeProgressionStorage(StorageType type, IEncryptionService encryptionService, ISerializationService serializationService)
        {
            return type switch
            {
                StorageType.Local => new LocalProgressionStorage(encryptionService, serializationService),
                StorageType.Cloud => new CloudProgressionStorage(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        #endregion
    }
}