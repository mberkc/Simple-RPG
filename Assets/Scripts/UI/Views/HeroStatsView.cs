using System.Globalization;
using Core;
using Data.ScriptableObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Views
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
        
        // Show the stats panel with data from a HeroCard
        internal void Show(HeroCardView heroCard)
        {
            if (isEnabled || heroCard == null) return;

            SetData(heroCard.HeroData);
            UpdatePosition(heroCard.transform.position);
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
            levelText.text = heroData.Level.ToString(CultureInfo.InvariantCulture);
            healthText.text = heroData.Health.ToString(CultureInfo.InvariantCulture);
            attackPowerText.text = heroData.AttackPower.ToString(CultureInfo.InvariantCulture);
            experienceText.text = heroData.Experience.ToString(CultureInfo.InvariantCulture);
        }

        private void UpdatePosition(Vector3 transformPosition)
        {
            panelTransform.position = transformPosition;
        }

        private void EnableAnimation(bool enable)
        {
            isEnabled = enable;
            canvasGroup.DOKill(true);
            canvasGroup.DOFade(enable ? 1f : 0f, Constants.NormalAnimationSpeed);
        }
    }
}
