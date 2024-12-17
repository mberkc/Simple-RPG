using TMPro;
using UnityEngine;

namespace UI.Views
{
    public class HeroStatsView : MonoBehaviour
    {
        
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI attackPowerText;
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private RectTransform panelTransform;

        private readonly Vector2 _topLeftCorner = new (0, 1);
        
        // Show the stats panel with data from a HeroCard
        internal void Show(HeroCardView heroCard)
        {
            if (heroCard == null) return;

            SetData(heroCard);
            UpdatePosition(heroCard.transform.position);
            EnableAnimation(true);
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
            panelTransform.anchorMin = _topLeftCorner;
            panelTransform.anchorMax = _topLeftCorner;
            panelTransform.pivot = _topLeftCorner;
            panelTransform.position = transformPosition;
            // TODO position
        }

        private void EnableAnimation(bool enable)
        {
            transform.localScale = enable? Vector3.one : Vector3.zero;
        }
    }
}
