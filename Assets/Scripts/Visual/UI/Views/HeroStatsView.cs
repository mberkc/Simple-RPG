using System.Globalization;
using Core;
using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;

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
        
        internal void Show(HeroData heroData, Vector3 screenPosition)
        {
            if (isEnabled) return;

            SetData(heroData);
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
