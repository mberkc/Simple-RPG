using Core;
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

            SetData(heroCard);
            UpdatePosition(heroCard.transform.position);
            EnableAnimation(true);
        }

        internal void Hide()
        {
            if(!isEnabled) return;
            
            EnableAnimation(false);
        }

        private void SetData(HeroCardView heroCard)
        {
            nameText.text = heroCard.HeroName;
            levelText.text = "1"; // TODO
            healthText.text = heroCard.Health.ToString();
            attackPowerText.text = heroCard.AttackPower.ToString();
            experienceText.text = "0"; // TODO
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
