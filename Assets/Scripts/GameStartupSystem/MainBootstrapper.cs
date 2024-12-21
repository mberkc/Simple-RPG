using System;
using System.Threading.Tasks;
using Core;
using Core.Encryption;
using Core.Progression;
using Core.Progression.ProgressionStorage;
using Core.Serialization;
using Data;
using Data.ScriptableObjects;
using GameLogic;
using GameStartupSystem.Bootstrapper;
using UnityEngine;

namespace GameStartupSystem
{
    public class MainBootstrapper : GameBootstrapper
    {
        [SerializeField] private StorageType storageType = StorageType.Local;
        [SerializeField] private EncryptionType encryptionType = EncryptionType.AES;
        [SerializeField] private SerializationType serializationType = SerializationType.JSON;
        [SerializeField] private string encryptionKey = "0e2tk8M7nbH1pS5z"; // Must be 16 characters for AES

        [SerializeField] private HeroData[] heroes;
        [SerializeField] private EnemyData[] enemies; 
        
        private GameStateManager _gameStateManager;
        private EntityService _entityService;
        private SceneTransitionManager _sceneTransitionManager;
        private GameFlowManager _gameFlowManager;
        
        public override async void Initialize()
        {
            try
            {
                Debug.Log("Initializing Main Bootstrapper!");
                
                var progressionService = await InitializeProgressionService();
                _gameStateManager = new GameStateManager(new GameState(), progressionService);
                _entityService = new EntityService(heroes, enemies);
                _sceneTransitionManager = new SceneTransitionManager();

                //RegisterServices(_gameStateManager, _entityService, _sceneTransitionManager);

                await _gameStateManager.InitializeGameStateAsync();

                _gameFlowManager = new GameFlowManager(_gameStateManager, _sceneTransitionManager);
                
                InitializationCompletionSource.TrySetResult(true);
                Debug.Log("Main Bootstrapper initialized!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Main Bootstrapper initialization failed! Exception: {e.Message}");
                InitializationCompletionSource.TrySetException(e);
            }
        }

        #region Progression Service

        private async Task<ProgressionService> InitializeProgressionService()
        {
            var encryptionService = InitializeEncryptionService(encryptionType);
            var serializationService = InitializeSerializationService(serializationType);
            var progressionStorage = InitializeProgressionStorage(storageType, encryptionService, serializationService);
            var progressionService = new ProgressionService(progressionStorage);
            await progressionService.LoadProgressionAsync();
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

        /*
        private void RegisterServices(GameStateManager gameStateManager, EntityService entityService, SceneTransitionManager sceneTransitionManager)
        {
            throw new NotImplementedException();
        }
        */

        #region SceneBootstrapper Injection Methods
        
        internal GameState GetGameState()
        {
            return _gameStateManager.GetGameState();
        }
        
        internal EntityService GetEntityService()
        {
            return _entityService;
        }

        #endregion
    }
}