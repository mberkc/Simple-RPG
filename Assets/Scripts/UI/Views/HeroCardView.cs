using System;
using Core;
using Data.ScriptableObjects;
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
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image selectionBorder;
        
        internal HeroData HeroData { get; private set; }

        private bool isSelected = false;

        internal void Initialize(HeroData heroData)
        {
            HeroData = heroData;

            backgroundImage.color = heroData.Color;
            nameText.text = heroData.EntityName;
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