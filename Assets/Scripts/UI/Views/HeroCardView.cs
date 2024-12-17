using System;
using Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PointerEventUtility;

namespace UI.Views
{
    public class HeroCardView : PointerEventForwarderTarget
    {
        internal event Action<HeroCardView> OnHeroSelected, OnHeroDeselected, OnHeroHold;

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
            SetSelected(false);
        }

        internal void SetSelected(bool selected)
        {
            isSelected = selected;
            
            selectionBorder.DOKill(true);
            selectionBorder.DOFade(selected ? 1f : 0f, Constants.NormalAnimationSpeed);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            OnCardClicked();
            Invoke(nameof(HoldActionComplete), 3f);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            HoldActionCancel();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            HoldActionCancel();
        }
        
        private void OnCardClicked()
        {
            if (isSelected)
                OnHeroDeselected?.Invoke(this);
            else
                OnHeroSelected?.Invoke(this);
        }

        private void HoldActionCancel()
        {
            CancelInvoke(nameof(HoldActionComplete));
            OnHeroHold?.Invoke(null);
        }

        private void HoldActionComplete()
        {
            OnHeroHold?.Invoke(this);
        }
    }
}