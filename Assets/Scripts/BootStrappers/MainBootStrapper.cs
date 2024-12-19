using System;
using System.Threading.Tasks;
using Core;
using Core.BootStrapper;
using Core.Encryption;
using Core.Progression;
using Core.Progression.ProgressionStorage;
using Core.Serialization;
using Data;
using Data.ScriptableObjects;
using GameLogic;
using GameLogic.State;
using UnityEngine;

namespace BootStrappers
{
    public class MainBootStrapper : GameBootStrapper
    {
        [SerializeField] private StorageType storageType = StorageType.Local;
        [SerializeField] private EncryptionType encryptionType = EncryptionType.AES;
        [SerializeField] private SerializationType serializationType = SerializationType.JSON;
        [SerializeField] private string encryptionKey = "0e2tk8M7nbH1pS5z"; // Must be 16 characters for AES

        [SerializeField] private HeroData[] heroes;
        [SerializeField] private EnemyData[] enemies; 
        
        // Cache if needed
        //private ProgressionService progressionService;
        //private StateManager stateManager;
        //private GameFlowManager gameFlowManager;
        
        public override async void Initialize()
        {
            try
            {
                Debug.Log("Initializing Main BootStrapper!");
                
                var progressionService = await InitializeProgressionService();
                
                if(!InitializeEntityResources()) return;
            
                InitializeManagersAndInjectDependencies(progressionService);

                Debug.Log("Main BootStrapper initialized!");
                InitializationCompletionSource.TrySetResult(true);
            }
            catch (Exception e)
            {
                Debug.LogError($"Main BootStrapper initialization failed! Exception: {e.Message}");
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
        
        private bool InitializeEntityResources()
        {
            var heroAmount = heroes.Length;
            var enemyAmount = enemies.Length;
            Debug.Log($"Available Heroes: {heroAmount}");
            Debug.Log($"Available Enemies: {enemyAmount}");
            if (heroAmount == Constants.TotalHeroes && enemyAmount == Constants.TotalEnemies)
            {
                EntityDatabase.Initialize(heroes, enemies);
                return true;
            }
            
            Debug.LogError("Please assign all Heroes and Enemies to Main BootStrapper!");
            return false;
        }

        private void InitializeManagersAndInjectDependencies(ProgressionService progressionService)
        {
            new StateManager().Initialize(progressionService);
            new GameFlowManager().Initialize(progressionService);
            // Inject progression service to UI?
        }
    }
}