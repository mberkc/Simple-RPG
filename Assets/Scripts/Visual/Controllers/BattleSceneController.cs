using System.Collections.Generic;
using Core;
using Core.EventManager.UIEventManager;
using Data;
using TMPro;
using UnityEngine;
using Visual.Rendering.EntityRenderer;
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

        private BattleEnemyRenderer enemyEntityRenderer;
        private BattleHeroRenderer[] heroEntityRenderers = new BattleHeroRenderer[3];
        public void Initialize(UserData userData, EnemyService enemyService, EntityRendererFactory entityRendererFactory)
        {
            Debug.Log("Initializing Battle Scene Controller");
            levelText.text = $"Level: {userData.CurrentLevel}";
            InitializeEnemyEntityView(userData.CurrentLevel, enemyService, entityRendererFactory);
            InitializeHeroEntityViews(userData, entityRendererFactory);
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
            UIEventManager.OnPlayerTurnStarted += PlayerTurnStarted;
            UIEventManager.OnPlayerTurnEnded += PlayerTurnEnded;
            UIEventManager.OnBattleComplete += ShowResult;
        }
        
        private void UnsubscribeEvents()
        {
            UIEventManager.OnEntityAttacked -= EntityAttack;
            UIEventManager.OnEntityDamaged -= EntityTakeDamage;
            UIEventManager.OnEntityDied -= EntityDie;
            UIEventManager.OnPlayerTurnStarted -= PlayerTurnStarted;
            UIEventManager.OnPlayerTurnEnded -= PlayerTurnEnded;
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
        }

        private void EntityDie(int boardIndex)
        {
            if(boardIndex == Constants.EnemyBoardIndex)
                enemyEntityRenderer.Die();
            else
            {    
                var heroRenderer = heroEntityRenderers[boardIndex];
                heroRenderer.OnHeroSelected -= OnHeroSelected;
                heroRenderer.OnHeroHold -= OnHeroHold;
                heroRenderer.Die();
            }
        }
        
        private void PlayerTurnStarted()
        {
            // TODO: Visual Update
        }

        private void PlayerTurnEnded()
        {
            // TODO: Visual Update
        }

        private void ShowResult(bool victory, List<int> experienceGainedHeroes)
        {
            battleResultView.ShowResult(victory, experienceGainedHeroes);
        }
        
        #endregion
        
        private void InitializeEnemyEntityView(int level, EnemyService enemyService, EntityRendererFactory entityRendererFactory)
        {
            var enemyData = enemyService.GetEnemyByLevel(level);
            enemyEntityRenderer = entityRendererFactory.CreateEnemy(enemyData, opponentEntityParent);
        }
        
        private void InitializeHeroEntityViews(UserData userData, EntityRendererFactory entityRendererFactory)
        {
            var selectedHeroIndexes = userData.SelectedHeroIndexes;
            var heroCount = playerEntityParent.childCount;
            if (selectedHeroIndexes == null || selectedHeroIndexes.Count != heroCount) return;
            
            for (var i = 0; i < heroCount; i++)
            {
                var heroIndex = selectedHeroIndexes[i];
                var heroData = userData.GetHeroData(heroIndex);
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
            if(entityRenderer != null)
                heroStatsView.Show(entityRenderer);
            else
                heroStatsView.Hide();
        }
    }
}