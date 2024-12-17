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
        
        // Show the stats panel with data from a HeroCard
        internal void Show(HeroCardView heroCard, bool isHovered = false)
        {
            if (heroCard == null) return;

            SetData(heroCard);
            UpdatePosition(heroCard.transform.position);
            EnableAnimation(true, isHovered);
        }

        internal void Hide()
        {
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

        private void EnableAnimation(bool enable, bool isHovered = false)
        {
            canvasGroup.DOKill(true);
            //canvasGroup.interactable = enable && !isHovered;
            //canvasGroup.blocksRaycasts = enable && !isHovered;
            if (isHovered)
                canvasGroup.DOFade(0.7f, Constants.FastAnimationSpeed);
            else
                canvasGroup.DOFade(enable ? 1f : 0f, Constants.NormalAnimationSpeed);
        }
    }
}
