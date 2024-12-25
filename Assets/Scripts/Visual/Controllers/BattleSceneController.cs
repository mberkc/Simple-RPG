using System.Collections.Generic;
using Core;
using Core.EventManager.UIEventManager;
using Data;
using TMPro;
using UnityEngine;
using Visual.Rendering;
using Visual.UI.Views;
using Visual.UI.Views.Battle;

namespace Visual.Controllers
{
    public class BattleSceneController : MonoBehaviour 
    {
        [SerializeField] private Transform playerEntityParent;
        [SerializeField] private Transform opponentEntityParent;
        [SerializeField] private BattleResultView battleResultView;
        [SerializeField] private HeroStatsView heroStatsView;
        [SerializeField] private TextMeshProUGUI levelText;

        private BattleEntityRenderer enemyEntityRenderer;
        private BattleHeroRenderer[] heroEntityRenderers = new BattleHeroRenderer[3];
        public void Initialize(UserData userData, EntityService entityService, EntityRendererFactory entityRendererFactory)
        {
            // TODO
            Debug.Log("Initializing Battle Scene Controller");
            levelText.text = $"Level: {userData.CurrentLevel}";
            InitializeEnemyEntityView(userData.CurrentLevel, entityService, entityRendererFactory);
            InitializeHeroEntityViews(userData.SelectedHeroIndexes, entityService, entityRendererFactory);
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
        
        #region Events
        
        private void SubscribeEvents()
        {
            UIEventManager.OnEntityAttacked += EntityAttack;
            UIEventManager.OnEntityDamaged += EntityTakeDamage;
            UIEventManager.OnEntityDied += EntityDie;
            UIEventManager.OnPlayerTurnEnded += PlayerTurnEnded;
            UIEventManager.OnOpponentTurnEnded += OpponentTurnEnded;
            UIEventManager.OnBattleComplete += ShowResult;
        }
        
        private void UnsubscribeEvents()
        {
            UIEventManager.OnEntityAttacked -= EntityAttack;
            UIEventManager.OnEntityDamaged -= EntityTakeDamage;
            UIEventManager.OnEntityDied -= EntityDie;
            UIEventManager.OnPlayerTurnEnded -= PlayerTurnEnded;
            UIEventManager.OnOpponentTurnEnded -= OpponentTurnEnded;
            UIEventManager.OnBattleComplete -= ShowResult;
        }

        private void EntityAttack(int boardIndex)
        {
            if(boardIndex == Constants.EnemyBoardIndex)
                enemyEntityRenderer.Attack();
            else
                heroEntityRenderers[boardIndex].Attack();
        }
        
        private void EntityTakeDamage(int boardIndex, float damage, float targetHealth)
        {
            if(boardIndex == Constants.EnemyBoardIndex)
                enemyEntityRenderer.TakeDamage(damage, targetHealth);
            else
                heroEntityRenderers[boardIndex].TakeDamage(damage, targetHealth);
            ShowDamageAnimation(boardIndex, damage);
        }

        private void EntityDie(int boardIndex)
        {
            if(boardIndex == Constants.EnemyBoardIndex)
                enemyEntityRenderer.Die();
            else
                heroEntityRenderers[boardIndex].Die();
        }

        private void PlayerTurnEnded()
        {
            // TODO
        }
        
        private void OpponentTurnEnded()
        {
            // TODO
        }

        private void ShowResult(bool victory, List<int> aliveHeroIndexes)
        {
            battleResultView.ShowResult(victory);
        }
        
        #endregion

        private void ShowDamageAnimation(int boardIndex, float damage)
        {
            // boardIndex == Constants.EnemyBoardIndex ? enemyEntityRenderer : heroEntityRenderers[boardIndex];
            // Spawn at specific position
        }
        
        private void InitializeEnemyEntityView(int level, EntityService entityService, EntityRendererFactory entityRendererFactory)
        {
            var enemyData = entityService.GetEnemyByLevel(level);
            enemyEntityRenderer = entityRendererFactory.CreateEnemy(enemyData, opponentEntityParent);
        }
        
        private void InitializeHeroEntityViews(List<int> selectedHeroIndexes, EntityService entityService, EntityRendererFactory entityRendererFactory)
        {
            var heroCount = playerEntityParent.childCount;
            if (selectedHeroIndexes == null || selectedHeroIndexes.Count != heroCount) return;
            
            for (var i = 0; i < heroCount; i++)
            {
                var heroIndex = selectedHeroIndexes[i];
                var heroData = entityService.GetHeroByIndex(heroIndex);
                var heroRenderer = entityRendererFactory.CreateHero(heroData, playerEntityParent.GetChild(i), i);
                heroRenderer.OnHeroSelected += OnHeroSelected;
                heroRenderer.OnHeroHold += OnHeroHold;
                heroEntityRenderers[i] = heroRenderer;
            }
        }

        private void OnHeroSelected(int boardIndex)
        {
            UIEventManager.RaisePlayerAttackRequested(boardIndex)?.Invoke();
        }
        
        private void OnHeroHold(BattleHeroRenderer entityRenderer)
        {
            // Position hero stats views related to entityRenderer, but still it will be child of battleUICanvas
            // Show hero statsviews
            if(entityRenderer != null)
                heroStatsView.Show(entityRenderer);
            else
                heroStatsView.Hide();
        }

#if UNITY_EDITOR
        
        // TODO: Remove after Test

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) OnHeroSelected(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) OnHeroSelected(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) OnHeroSelected(2);
            if (Input.GetKeyDown(KeyCode.Q)) OnHeroHold(heroEntityRenderers[0]);
            if (Input.GetKeyDown(KeyCode.W)) OnHeroHold(heroEntityRenderers[1]);
            if (Input.GetKeyDown(KeyCode.E)) OnHeroHold(heroEntityRenderers[2]);
            if (Input.GetKeyDown(KeyCode.A)) OnHeroHold(null);
        }
        
#endif
    }
}