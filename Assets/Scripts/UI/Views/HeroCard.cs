using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class HeroCard : MonoBehaviour
    {
        public event Action<HeroCard> OnHeroSelected;
        public event Action<HeroCard> OnHeroDeselected;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI statsText;
        [SerializeField] private Button cardButton;
        [SerializeField] private Image selectionBorder;

        private bool isSelected = false;

        public string HeroName { get; private set; }
        public int Health { get; private set; }
        public int AttackPower { get; private set; }

        public void Initialize(string heroName, int health, int attackPower)
        {
            HeroName = heroName;
            Health = health;
            AttackPower = attackPower;

            nameText.text = HeroName;
            statsText.text = $"HP: {Health} | AP: {AttackPower}";
            selectionBorder.enabled = false;

            cardButton.onClick.AddListener(OnCardClicked);
        }

        private void OnCardClicked()
        {
            if (isSelected)
                OnHeroDeselected?.Invoke(this);
            else
                OnHeroSelected?.Invoke(this);
        }

        public void SetSelected(bool selected)
        {
            isSelected = selected;
            selectionBorder.enabled = isSelected;
        }
    }
}