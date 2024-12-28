using System;
using System.Globalization;
using Core;
using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Visual.Rendering;
using Visual.UI.Views.HeroSelection;

namespace Visual.UI.Views
{
    public class HeroStatsView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI attackPowerText;
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private RectTransform panelTransform;

        private bool isEnabled = false;
        
        // Show the stats panel with data of a HeroCard => UI Element
        internal void Show(HeroCardView heroCard)
        {
            if (isEnabled || heroCard == null) return;

            SetData(heroCard.HeroData);
            UpdatePosition(heroCard.transform.position);
            EnableAnimation(true);
        }
        
        // Show the stats panel with data of a HeroEntity => SpriteRenderer
        internal void Show(BattleHeroRenderer heroEntity)
        {
            if (isEnabled || heroEntity == null) return;

            SetData(heroEntity.HeroData);
            var screenPosition = VisualUtility.WorldToScreenPosition(heroEntity.transform.position);  
            UpdatePosition(screenPosition);
            EnableAnimation(true);
        }


        internal void Hide()
        {
            if(!isEnabled) return;
            
            EnableAnimation(false);
        }

        private void SetData(HeroData heroData)
        {
            nameText.text = heroData.EntityName;
            levelText.text = heroData.UserHeroData.Level.ToString(CultureInfo.InvariantCulture);
            healthText.text = heroData.ModifiedHealth.ToString(CultureInfo.InvariantCulture);
            attackPowerText.text = heroData.ModifiedAttackPower.ToString(CultureInfo.InvariantCulture);
            experienceText.text = heroData.UserHeroData.Experience.ToString(CultureInfo.InvariantCulture);
        }

        private void EnableAnimation(bool enable)
        {
            isEnabled = enable;
            canvasGroup.DOKill(true);
            canvasGroup.DOFade(enable ? 1f : 0f, Constants.NormalAnimationDuration);
        }

        private void UpdatePosition(Vector2 screenPosition)
        {
            panelTransform.position = screenPosition;
        }

        private void OnDestroy()
        {
            canvasGroup.DOKill();
        }
    }
}
