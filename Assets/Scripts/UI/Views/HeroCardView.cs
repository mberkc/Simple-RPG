using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Views
{
    public class HeroCardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        internal event Action<HeroCardView> OnHeroSelected, OnHeroDeselected, OnHeroHovered;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Button cardButton;
        [SerializeField] private Image selectionBorder;

        internal string HeroName { get; private set; }
        internal int Health { get; private set; }
        internal int AttackPower { get; private set; }

        private bool isSelected = false;

        internal void Initialize(string heroName, int health, int attackPower)
        {
            HeroName = heroName;
            Health = health;
            AttackPower = attackPower;

            nameText.text = HeroName;
            selectionBorder.enabled = false;

            cardButton.onClick.AddListener(OnCardClicked);
        }

        internal void SetSelected(bool selected)
        {
            isSelected = selected;
            selectionBorder.enabled = isSelected;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHeroHovered?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHeroHovered?.Invoke(null);
        }
        
        private void OnCardClicked()
        {
            if (isSelected)
                OnHeroDeselected?.Invoke(this);
            else
                OnHeroSelected?.Invoke(this);
        }
    }
}